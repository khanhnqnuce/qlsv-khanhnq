$(document).ready(function () {

    var url = "Home/Sortable";
    $('#sortableCompleted, #sortableTodo').sortable({
        update: function (event, ui) {
            
            var array = { array1: $("#sortableTodo").sortable("toArray").join(","), array2: $("#sortableCompleted").sortable("toArray").join(",") }
            $('#sortableCompleted, #sortableTodo').sortable('disable');
            $.post(url, array, function (dt, status) {
                alert(dt);
                //$('#sortableCompleted, #sortableTodo').sortable('enable');
            });
        }
    });

    $('body').on('click', '#addTaskButton', function () {
        var id = new Date().getTime();
        $("#sortableTodo").append(myFunction3(id, $("#addTaskTextField").val()));
        $("#addTaskTextField").val("");
    });

    $('body').on('click', '.remove', function () {

        var parentContainer = $(this).parent().parent().parent();
        //var check = parentContainer.attr('data-role');

        var taskId = parentContainer.attr('id');
        var nd = parentContainer.attr('title');
        //var a = $('#' + taskId).html();
        var b = myFunction1(taskId, nd);
        $('#sortableTodo #' + taskId).remove();
        $("#sortableCompleted").append(b);
    });

    $('body').on('click', '.remove1', function () {

        var parentContainer = $(this).parent().parent().parent();
        //var check = parentContainer.attr('data-role');

        var taskId = parentContainer.attr('id');
        var nd = parentContainer.attr('title');
        //var a = $('#' + taskId).html();
        var b = myFunction2(taskId, nd);

        $('#sortableCompleted #' + taskId).remove();
        $("#sortableTodo").append(b);


    });
});
function myFunction1(id, nd) {
    return ('<li id="' + id + '" title="' + nd + '" style="background-color: rgba(255,255,255,0.95)"><div id="taskContainer"><div id="taskButtonBar"><a href="javascrip:" class="remove1"><span id="remove-button"></span></a></div><div id="taskLabel"><span id="small-task"></span>' + nd + '</div></div></li>');
}

function myFunction2(id, nd) {
    return ('<li id="' + id + '" title="' + nd + '" style="background-color: rgba(255,255,255,0.95)"><div id="taskContainer"><div id="taskButtonBar"><a href="javascrip:" class="remove"><span id="remove-button"></span></a></div><div id="taskLabel"><span id="small-task"></span>' + nd + '</div></div></li>');
}

function myFunction3(id, nd) {
    return ('<li id="' + id + '" title="' + nd + '" style="background-color: rgba(255,255,255,0.95)"><div id="taskContainer"><div id="taskButtonBar"><a href="javascrip:" class="remove"><span id="remove-button"></span></a></div><div id="taskLabel"><span id="small-task"></span>' + nd + '</div></div></li>');
}