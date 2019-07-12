var app = new Vue({
    el: '#appProfilOption',
    data: {
        collections: [],
        editId: 0,
        entityType: "",
        entityName: "",
        isLoading: true
    },
    methods: {
        getAllProfileOption: function () {
            this.isLoading = true;
            var that = this;
            $.ajax({
                url: '/api/ProfilOption/GetAll',
                contentType: 'application/json',
                method: 'GET',
                success: function (response) {
                    that.collections = response;

                    $("#mainDiv").show();
                    that.isLoading = false;
                }
            })
        },
        create: function (name) {
            this.isLoading = true;
            var that = this;
            $.ajax({
                type: "POST",
                contentType: 'application/json',
                data: JSON.stringify({ name: $('#add' + name).val() }),
                url: '/api/' + name + '/Create',
                success: function () {

                    $.ajax({
                        url: '/api/' + name + '/GetAll',
                        method: 'GET',
                        contentType: 'application/json',
                        success: function (response) {
                            Vue.set(that.collections.options, name, response);

                            that.isLoading = false;
                        }
                    });

                    $("#add" + name).val('');
                    $("#addError" + name).hide();
                },
                error: function (response) {

                    try {
                        var message = response.responseJSON.errors.Name[0];
                        $("#addError" + name).children('span').text(message);
                        $("#addError" + name).show();
                    }
                    catch {
                        $("#addError" + name).children('span').text("Une erreur inatendu est survenu");
                        $("#addError" + name).show();
                    }

                    that.isLoading = false;
                }
            })
        },
        edit: function (name, id) {
            this.isLoading = true;
            var that = this;
            $.ajax({
                url: '/api/' + name + '/Update/' + id,
                type: "PUT",
                contentType: 'application/json',
                data: JSON.stringify({ id: id, name: $('#entityName').val() }),
                success: function () {

                    $.ajax({
                        url: '/api/' + name + '/GetAll',
                        method: 'GET',
                        contentType: 'application/json',
                        success: function (response) {
                            Vue.set(that.collections.options, name, response);

                            that.isLoading = false;
                        }
                    });

                    $('#editOption').modal('toggle');

                },
                error: function (response) {

                    try {
                        var message = response.responseJSON.errors.Name[0];
                        $("#editModalError").children('span').text(message);
                        $("#editModalError").show();
                    }
                    catch {
                        $("#editModalError").children('span').text("Une erreur inatendu est survenu");
                        $("#editModalError").show();
                    }

                    that.isLoading = false;
                }
            })
        },
        deactivate: function (name, id) {
            if (confirm("Êtes-vous sur de vouloir supprimer cette entitée?")) {
                this.isLoading = true;
                var that = this;
                $.ajax({
                    type: "DELETE",
                    contentType: 'application/json',
                    url: '/api/' + name + '/Delete/' + id,
                    success: function () {

                        $.ajax({
                            url: '/api/' + name + '/GetAll',
                            method: 'GET',
                            contentType: 'application/json',
                            success: function (response) {
                                Vue.set(that.collections.options, name, response);

                                that.isLoading = false;
                            }
                        });

                        $("#error" + name + id).hide();

                    },
                    error: function (response) {

                        try {
                            var message = response.responseJSON.errors[0].errorMessage;
                            $("#error" + name + id).children('span').text(message);
                            $("#error" + name + id).show();
                        }
                        catch {
                            $("#error" + name + id).children('span').text("Une erreur inatendu est survenu");
                            $("#error" + name + id).show();
                        }

                        that.isLoading = false;
                    }
                })
            }
        },
        onEditClick: function (name, id) {
            this.editId = id;
            this.entityType = name;
            this.entityName = $('#' + name + id).text();
            $("#editModalError").children('span').text("");
        },
        toFrench: function (name) {
            return this.collections.traductions[name];
        },
        close: function (id) {
            $('#' + id).hide();
        }
    }
});

app.getAllProfileOption();
