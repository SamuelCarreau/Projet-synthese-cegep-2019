var select = function (normalizeName) {
    var element = $("#" + normalizeName);
    if (element.hasClass("active")) {
        element.removeClass("active");
    }
    else {
        element.addClass("active");
    }
}

var send = function (userId) {
    var list = [];

    $("#roles").children(".active").each(function (i, v) {
        list.push(v.attributes.rolename.nodeValue);
    });

    $.ajax({
        url: "/api/UsersApi/UpdatePermission/" + userId,
        type: "PUT",
        contentType: 'application/json',
        data: JSON.stringify(list),
        success: function () {
            $("#successMessage").show();
            $("#errorMessage").hide();
        },
        error: function (reponse) {
            console.log(reponse);
            $("#successMessage").hide();
            $("#errorMessage").show();
        }
    });
}