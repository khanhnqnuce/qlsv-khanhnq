using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FDI.DA.Admin
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Truy vấn Order by
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, string propertyName)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            // DataSource control passes the sort parameter with a direction
            // if the direction is descending          
            var descIndex = propertyName.IndexOf(" DESC", StringComparison.Ordinal);
            if (descIndex >= 0)
            {
                propertyName = propertyName.Substring(0, descIndex).Trim();
            }

            if (String.IsNullOrEmpty(propertyName))
            {
                return source;
            }

            var parameter = Expression.Parameter(source.ElementType, String.Empty);
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = (descIndex < 0) ? "OrderBy" : "OrderByDescending";

            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                new Type[] { source.ElementType, property.Type },
                                                source.Expression, Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(methodCallExpression);
        }

        /// <summary>
        /// Hàm lấy qua GridRequest
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="request"></param>
        /// <param name="totalRecord"> </param>
        /// <returns></returns>
        public static IQueryable<T> SelectByRequest<T>(this IQueryable<T> source, ParramRequest request, ref int totalRecord)
        {
            string propertyName;

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (!string.IsNullOrEmpty(request.Keyword) && request.SearchInField.Count > 1)
            {
                IQueryable<T> queryResult = null;
                var totalRecordTemp = 0;
                if (request.SearchInField.Count == 1)
                    source.Has(request.SearchInField[0], request.Keyword, ref totalRecord);
                else
                {
                    var indexVar = -1;
                    foreach (var propSearch in request.SearchInField)
                    {
                        indexVar++;
                        if (queryResult == null)
                        {
                            queryResult = source.Has(request.SearchInField[indexVar], request.Keyword, ref totalRecordTemp).AsQueryable();
                        }
                        else
                        {
                            var empty = !queryResult.Any();
                            queryResult = !empty ? queryResult.AsEnumerable().Union(source.Has(request.SearchInField[indexVar], request.Keyword, ref totalRecordTemp).AsEnumerable()).AsQueryable() : source.Has(request.SearchInField[indexVar], request.Keyword, ref totalRecordTemp).AsQueryable();
                        }
                        totalRecord += totalRecordTemp;
                    }
                }
                source = queryResult;
            }
            else if (!string.IsNullOrEmpty(request.Keyword) && request.SearchInField.Count == 1)
            {
                source = source.HasOne(request.SearchInField[0], request.Keyword);
                totalRecord = source.Count();
            }
            if (totalRecord == 0 && source != null)
                totalRecord = source.Count();
            #region dùng cho việc Order
             
            if (source != null && (string.IsNullOrEmpty(request.FieldSort) && !source.ToString().ToLower().Contains("order by")))
            {
                request.FieldSort = source.ElementType.GetProperties()[1].Name;
                request.TypeSort = true;
            }
            if (!string.IsNullOrEmpty(request.FieldSort))
            {
                propertyName = request.FieldSort;
                request.TypeSort = true;
            }
            else
            {
                var match = Regex.Match(source.ToString().ToUpper(), @"ORDER\s*BY\s*(?:\x5B[^^\x5D]+\x5D\x2E\x5B)?(?<col>[^\x5D]+)\x5D\s*DESC$", RegexOptions.IgnoreCase);
                var colSearch = match.Groups["col"].Value;
                propertyName = !string.IsNullOrEmpty(colSearch) ? colSearch : source.ElementType.GetProperties()[1].Name;
                if (match.ToString().ToLower().Contains("desc"))
                    request.TypeSort = true;
            }
            string methodName = (request.TypeSort) ? "OrderByDescending" : "OrderBy";

            var parameter = Expression.Parameter(source.ElementType, String.Empty);
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);
            Expression methodCallExpression = Expression.Call(typeof(Queryable), methodName,
                                                              new Type[] { source.ElementType, property.Type },
                                                              source.Expression, Expression.Quote(lambda));
            source = source.Provider.CreateQuery<T>(methodCallExpression);

            #endregion

            //  totalRecord = source.Count();

            if (request.CurrentPage > 0 && request.RowPerPage > 0)
            {

                methodCallExpression = Expression.Call(
                    typeof(Queryable), "Skip",
                    new Type[] { source.ElementType },
                    source.Expression, Expression.Constant((request.CurrentPage - 1) * request.RowPerPage));
                source = source.Provider.CreateQuery<T>(methodCallExpression);

                methodCallExpression = Expression.Call(
                    typeof(Queryable), "Take",
                    new Type[] { source.ElementType },
                    source.Expression, Expression.Constant(request.RowPerPage));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
            }
            return source;
        }


        //public static IQueryable<T> Has<T>(this IQueryable<T> source, List<string> propertyNames, string keyword)
        //{
        //    if (source == null || propertyNames == null || propertyNames.Count == 0 || string.IsNullOrEmpty(keyword))
        //    {
        //        return source;
        //    }
        //    keyword = keyword.ToLower();
        //    MethodCallExpression returnMethodCallExpression = source.Expression.;
        //    foreach (var propertyName in propertyNames)
        //    {
        //        var parameter = Expression.Parameter(source.ElementType, String.Empty);
        //        var property = Expression.Property(parameter, propertyName);
        //        var CONTAINS_METHOD = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        //        var TO_LOWER_METHOD = typeof(string).GetMethod("ToLower", new Type[] { });

        //        var toLowerExpression = Expression.Call(property, TO_LOWER_METHOD);
        //        var termConstant = Expression.Constant(keyword, typeof(string));
        //        var containsExpression = Expression.Call(toLowerExpression, CONTAINS_METHOD, termConstant);

        //        var predicate = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

        //        var methodCallExpression = Expression.Call(typeof(Queryable), "Where",
        //                                    new Type[] { source.ElementType },
        //                                    source.Expression, Expression.Quote(predicate));
        //    }
        //    return source.Provider.CreateQuery<T>(returnMethodCallExpression);
        //}


        public static IQueryable<T> HasOne<T>(this IQueryable<T> source, string propertyName, string keyword)
        {
            if (source == null || string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(keyword))
            {
                return source;
            }
            keyword = keyword.ToLower();

            var parameter = Expression.Parameter(source.ElementType, String.Empty);
            var property = Expression.Property(parameter, propertyName);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[] { });
            var typeProperty = property.Type.FullName;
            var termConstant = Expression.Constant(keyword, typeof(string));

            MethodCallExpression containsExpression = null;
            if (typeProperty == "System.String")
            {
                var toLowerExpression = Expression.Call(property, toLowerMethod);
                containsExpression = Expression.Call(toLowerExpression, containsMethod, termConstant);
            }
            else if (typeProperty == "System.Int32")
            {
                try
                {
                    var keywordInt = Convert.ToInt32(keyword);
                    termConstant = Expression.Constant(keywordInt, typeof(int));
                    containsMethod = typeof(int).GetMethod("Equals", new[] { typeof(int) });
                    containsExpression = Expression.Call(property, containsMethod, termConstant);
                }
                catch
                {
                    return Enumerable.Empty<T>().AsQueryable();
                }
            }
            else if (typeProperty == "System.Int64")
            {
                try
                {
                    var keywordInt = Convert.ToInt64(keyword);
                    termConstant = Expression.Constant(keywordInt, typeof(long));
                    containsMethod = typeof(long).GetMethod("Equals", new[] { typeof(long) });
                    containsExpression = Expression.Call(property, containsMethod, termConstant);
                }
                catch
                {
                    return Enumerable.Empty<T>().AsQueryable();
                }
            }
            else if (typeProperty == "System.Decimal")
            {
                try
                {
                    var keywordInt = Convert.ToDecimal(keyword);
                    termConstant = Expression.Constant(keywordInt, typeof(decimal));
                    containsMethod = typeof(decimal).GetMethod("Equals", new[] { typeof(decimal) });
                    containsExpression = Expression.Call(property, containsMethod, termConstant);
                }
                catch { source = Enumerable.Empty<T>().AsQueryable(); }
            }
            var predicate = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

            var methodCallExpression = Expression.Call(typeof(Queryable), "Where",
                                        new Type[] { source.ElementType },
                                        source.Expression, Expression.Quote(predicate));
            source = source.Provider.CreateQuery<T>(methodCallExpression);
            return source;
        }

        public static IQueryable<T> Has<T>(this IQueryable<T> source, string propertyName, string keyword, ref int totalRecordTemp)
        {
            if (source == null || string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(keyword))
            {
                return source;
            }
            keyword = keyword.ToLower();

            var parameter = Expression.Parameter(source.ElementType, String.Empty);
            var property = Expression.Property(parameter, propertyName);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var toLowerMethod = typeof(string).GetMethod("ToLower", new Type[] { });
            var typeProperty = property.Type.FullName;
            var termConstant = Expression.Constant(keyword, typeof(string));

            MethodCallExpression containsExpression = null;

            if (typeProperty == "System.String")
            {
                MethodCallExpression toLowerExpression = Expression.Call(property, toLowerMethod);
                containsExpression = Expression.Call(toLowerExpression, containsMethod, termConstant);
            }
            else if (typeProperty == "System.Int32")
            {
                try
                {
                    var keywordInt = Convert.ToInt32(keyword);
                    termConstant = Expression.Constant(keywordInt, typeof(int));
                    containsMethod = typeof(int).GetMethod("Equals", new[] { typeof(int) });
                    containsExpression = Expression.Call(property, containsMethod, termConstant);
                }
                catch { source = Enumerable.Empty<T>().AsQueryable(); }
            }
            else if (typeProperty == "System.Int64")
            {
                try
                {
                    var keywordInt = Convert.ToInt32(keyword);
                    termConstant = Expression.Constant(keywordInt, typeof(int));
                    containsMethod = typeof(int).GetMethod("Equals", new[] { typeof(int) });
                    containsExpression = Expression.Call(property, containsMethod, termConstant);
                }
                catch
                {
                    return Enumerable.Empty<T>().AsQueryable();
                }
            }
            else if (typeProperty == "System.Decimal")
            {
                try
                {
                    var keywordInt = Convert.ToDecimal(keyword);
                    termConstant = Expression.Constant(keywordInt, typeof(decimal));
                    containsMethod = typeof(decimal).GetMethod("Equals", new[] { typeof(decimal) });
                    containsExpression = Expression.Call(property, containsMethod, termConstant);
                }
                catch { source = Enumerable.Empty<T>().AsQueryable(); }
            }
            try
            {
                var predicate = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

                var methodCallExpression = Expression.Call(typeof(Queryable), "Where",
                                                                            new Type[] { source.ElementType },
                                                                            source.Expression, Expression.Quote(predicate));
                source = source.Provider.CreateQuery<T>(methodCallExpression);
                totalRecordTemp = source.Count();
            }
            catch
            {
                source = Enumerable.Empty<T>().AsQueryable();
            }
            return source;
        }
        /// <summary>
        /// Hàm where với LINQ
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="searchProperty"></param>
        /// <param name="searchOper"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        /// Cách dùng:
        /// list.Where("id", "eq", "12345");
        ///list.Where("id", "lt", "5468");
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> source, string searchProperty, string searchOper, string searchString)
    where TEntity : class
        {
            Type type = typeof(TEntity);

            ConstantExpression searchFilter = Expression.Constant(searchString);

            ParameterExpression parameter = Expression.Parameter(type, "p");
            PropertyInfo property = type.GetProperty(searchProperty);
            Expression propertyAccess = Expression.MakeMemberAccess(parameter, property);

            //support int?
            if (property.PropertyType == typeof(int?))
            {
                PropertyInfo valProp = typeof(int?).GetProperty("Value");
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, valProp);

                int? tn = Int32.Parse(searchString);
                searchFilter = Expression.Constant(tn);
            }

            //support decimal?
            if (property.PropertyType == typeof(decimal?))
            {
                var valProp = typeof(decimal?).GetProperty("Value");
                propertyAccess = Expression.MakeMemberAccess(propertyAccess, valProp);

                decimal? tn = Decimal.Parse(searchString);
                searchFilter = Expression.Constant(tn);
            }

            if (propertyAccess.Type == typeof(Int32))
                searchFilter = Expression.Constant(Int32.Parse(searchString));

            if (propertyAccess.Type == typeof(decimal))
                searchFilter = Expression.Constant(Decimal.Parse(searchString));


            MethodInfo startsWith = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
            MethodInfo endsWith = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
            MethodInfo contains = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

            Expression operation = null;

            switch (searchOper)
            {
                default:
                case "eq":
                    operation = Expression.Equal(propertyAccess, searchFilter);
                    break;
                case "ne":
                    operation = Expression.NotEqual(propertyAccess, searchFilter);
                    break;
                case "lt":
                    operation = Expression.LessThan(propertyAccess, searchFilter);
                    break;
                case "le":
                    operation = Expression.LessThanOrEqual(propertyAccess, searchFilter);
                    break;
                case "gt":
                    operation = Expression.GreaterThan(propertyAccess, searchFilter);
                    break;
                case "ge":
                    operation = Expression.GreaterThanOrEqual(propertyAccess, searchFilter);
                    break;
                case "bw":
                    operation = Expression.Call(propertyAccess, startsWith, searchFilter);
                    break;
                case "bn":
                    operation = Expression.Call(propertyAccess, startsWith, searchFilter);
                    operation = Expression.Not(operation);
                    break;
                case "ew":
                    operation = Expression.Call(propertyAccess, endsWith, searchFilter);
                    break;
                case "en":
                    operation = Expression.Call(propertyAccess, endsWith, searchFilter);
                    operation = Expression.Not(operation);
                    break;
                case "cn":
                    operation = Expression.Call(propertyAccess, contains, searchFilter);
                    break;
                case "nc":
                    operation = Expression.Call(propertyAccess, contains, searchFilter);
                    operation = Expression.Not(operation);
                    break;
            }

            var whereExpression = Expression.Lambda(operation, parameter);

            var resultExpression = Expression.Call(typeof(Queryable), "Where", new Type[] { source.ElementType }, source.Expression, whereExpression);

            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
