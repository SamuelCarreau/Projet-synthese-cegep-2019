var volunteeringTypeSelection = new Vue({
    el: "#volunteeringTypeSelection",
    data: {
        collection: []
    },
    methods: {
        getVolunteeringTypes: function () {
            var that = this;
            $.ajax({
                url: '/api/VolunteeringType/GetAll',
                type: 'GET',
                success: function (response) {
                    that.collection = response;
                }
            })
        },
       
    }
});

$(document).ready(volunteeringTypeSelection.getVolunteeringTypes());