var workshopTypeApp = new Vue({
    el: "#WorkshopTypeApp",
    data: {
        collection: []
    },
    methods: {
        getworkshoptypes: function () {
            that = this;
            $.ajax({
                url: "/api/WorkshopType/GetAll",
                type: "GET",
                success: function (response) {
                    that.collection = response;
                }
            })
        },
    }
});

workshopTypeApp.getworkshoptypes();