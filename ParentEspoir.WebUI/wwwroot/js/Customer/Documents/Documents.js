var documentTypeSelection = new Vue({
    el: "#documentTypeSelection",
    data: {
        collection: []
    },
    methods: {
        getDocumentTypes: function () {
            that = this;
            $.ajax({
                url: "/api/DocumentType/GetAll",
                type: "GET",
                success: function (response) {
                    that.collection = response;
                }
            })
        }
    }
});

documentTypeSelection.getDocumentTypes();