var notetypes = new Vue({
    el: "#NoteTypeApp",
    data: {
        collection: []
    },
    methods: {
        getnotetypes: function () {
            that = this;
            $.ajax({
                url: "/api/NoteType/GetAll",
                type: "GET",
                success: function (response) {
                    that.collection = response;
                }
            })
        }
    }
});

notetypes.getnotetypes();