using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace QLSV.Data.Utils.Data
{
    /// <summary>
    /// input chuỗi chuyền vào để ktra
    /// InputType kiểu kiểm tra
    /// Result lỗi là false, không lỗi là true
    /// MsgError tên lỗi
    /// </summary>
    public class InputParam
    {
        public string Input { get; set; }
        public InputType InputType { get; set; }
        public bool Result { get; set; }
        public string MsgError { get; set; }

        public InputParam()
        {
            Result = true;
        }
    }

    public enum InputType
    {
        SoNguyenDuong,
        SoNguyen,
        SoThucDuong,
        SoThuc,
        NgayThang,
        Email,
        ChuoiRong,
        KhongKiemTra
    }
    /// <summary>
    /// class validate có 2 biến Output lưu tất cả các listparam đã được check
    /// và biến Eorror lưu những kiểu inputparam có lỗi
    /// </summary>
    public class ValidateData
    {
        /// <summary>
        /// khai báo 2 biến kiểu inputparam
        /// </summary>
        public List<InputParam> Errors { get; private set; }
        public List<InputParam> Output { get; private set; }

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        public ValidateData()
        {
            Output = new List<InputParam>();
            Errors = new List<InputParam>();
        }

        private static InputParam Checkmail(string input)
        {
            var inputparm = new InputParam {InputType = InputType.Email};
            const string pattern = @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9]+(\.[a-z]{2,6})?(\.[a-z]{2,6})$";
            if (Regex.IsMatch(input,pattern))
            {
                inputparm.Result = true;
                inputparm.MsgError = "Không lỗi";
            }
            else
            {
                inputparm.Result = false;
                inputparm.MsgError = "Không đúng định dạng Email";
            }
            return inputparm;
        }

        private static InputParam CheckSoNguyenDuong(string input)
        {
            var inputparam = new InputParam
            {
                InputType = InputType.SoNguyenDuong
            };
            const string pattern = @"^\d*$"; 
            if (Regex.IsMatch(pattern, input))
            {
                inputparam.Result = true;
                inputparam.MsgError = "Không lỗi";
            }
            else
            {
                inputparam.Result = false;
                inputparam.MsgError = "Không phải số nguyên";
            }
            return inputparam;
        }

        private static InputParam CheckSoNguyen(string input)
        {
            var inputparam = new InputParam
            {
                InputType = InputType.SoNguyen
            };
            const string pattern = @"^-?\d*$";
            if (Regex.IsMatch(pattern, input))
            {
                inputparam.Result = true;
                inputparam.MsgError = "Không lỗi";
            }
            else
            {
                inputparam.Result = true;
                inputparam.MsgError = "Không phải số nguyên";
            }
            return inputparam;
        }

        private static InputParam CheckSoThuc(string input)
        {
            var inputParam = new InputParam
            {
                InputType = InputType.SoThuc
            };
            const string pattern = @"^-?\d*\.?\d*$";
            if (Regex.IsMatch(input, pattern))
            {
                inputParam.Result = true;
                inputParam.MsgError = "Không lỗi";
            }
            else
            {
                inputParam.Result = false;
                inputParam.MsgError = "Không phải số thực";
            }
            return inputParam;
        }

        private static InputParam CheckSoThucKhongAm(string input)
        {
            var inputparm = new InputParam
            {
                InputType = InputType.SoThucDuong
            };
            const string pattern = @"^\d*\.?\d*$";
            if (Regex.IsMatch(input, pattern))
            {
                inputparm.Result = true;
                inputparm.MsgError = "Không lỗi";
            }
            else
            {
                inputparm.Result = false;
                inputparm.MsgError = "Không phải số thực hoặc giá trị âm";
            }
            return inputparm;
        }

        private static InputParam CheckNgayThang(string input)
        {
            var inputParam = new InputParam
            {
                InputType = InputType.NgayThang
            };
            const string pattern = @"^\d{1,2}\/\d{1,2}\/\d\d\d\d$";
            if (Regex.IsMatch(input, pattern))
            {
                var dateStrings = input.Trim().Split('/');
                try
                {
                    // ReSharper disable once ObjectCreationAsStatement
                    new DateTime(int.Parse(dateStrings[2]), int.Parse(dateStrings[1]), int.Parse(dateStrings[0]));
                    inputParam.Result = true;
                    inputParam.MsgError = "Không lỗi";
                }
                catch (Exception)
                {
                    inputParam.Result = false;
                    inputParam.MsgError = "Ngày này không tồn tại";
                }
                //inputParam.Result = true;
                //inputParam.MsgError = "Không lỗi";
            }
            else
            {
                inputParam.Result = false;
                inputParam.MsgError = "Không phải ngày tháng";
            }
            return inputParam;
        }
        /// <summary>
        /// truyền vào 1 kiểu inputparam
        /// </summary>
        /// <returns>trả về 1 kiểu InputParam đã được check Error và Result</returns>
        public static InputParam ValDataOne(InputParam inputparam)
        {
            InputParam inputValue;
            if (inputparam.InputType == InputType.KhongKiemTra)
            {
                inputValue = inputparam;
                return inputValue;
            }
            if (string.IsNullOrEmpty(inputparam.Input))
            {
                inputparam.Result = false;
                inputparam.MsgError = "Giá trị null hoặc rỗng";
                inputValue = inputparam;
            }
            else
            {
                switch (inputparam.InputType)
                {
                    case InputType.SoNguyen:
                        {
                            inputValue = CheckSoNguyen(inputparam.Input);
                        }
                        break;
                    case InputType.SoNguyenDuong:
                        {
                            inputValue = CheckSoNguyenDuong(inputparam.Input);
                        }
                        break;
                    case InputType.SoThucDuong:
                        {
                            inputValue = CheckSoThucKhongAm(inputparam.Input);
                            break;
                        }
                    case InputType.SoThuc:
                        {
                            inputValue = CheckSoThuc(inputparam.Input);
                            break;
                        }
                    case InputType.NgayThang:
                        {
                            inputValue = CheckNgayThang(inputparam.Input);
                            break;
                        }
                    case InputType.Email:
                        {
                            inputValue = Checkmail(inputparam.Input);
                            break;
                        }
                    default:
                        {
                            inputValue = inputparam;
                            break;
                        }
                }
            }
            return inputValue;
        }
        /// <summary>
        /// Truyền vào kiểm tra 1 list các textbox
        /// </summary>
        /// <param name="inputValue">List kiểu Inputparam truyền vào Input và InputType</param>
        /// <returns>trả về list kiểu Inputparam Gồm result và thông báo lỗi Error</returns>
        public IList<InputParam> ValDataList(IEnumerable<InputParam> inputValue)
        {
            Errors = new List<InputParam>();
            Output = new List<InputParam>();
            foreach (var item in inputValue)
            {
                var a = ValDataOne(item);
                Output.Add(a);
                if (!a.Result)
                {
                    Errors.Add(a);
                }
            }
            return Output;
        }
    }
}
