var appSupportGroup = new Vue({
    el: '#UserSelection',
    data: {
        users: []
    },
    methods: {
        getUsers() {
            var that = this;
            $.ajax({
                url: '/api/UsersApi/GetAll',
                method: 'GET',
                success: function (response) {
                    that.users = response;
                }
            })
        },
        nameToString: function (user) {
            if (user.name == null) {
                return user.userName;
            }
            return user.name;
        }
    }
});

appSupportGroup.getUsers();