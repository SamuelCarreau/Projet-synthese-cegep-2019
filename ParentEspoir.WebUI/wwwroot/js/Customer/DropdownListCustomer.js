class option {
    constructor(name) {
        this.name = name;
        this.urlGetAll = '/api/' + name + '/GetAll';
        this.app = new Vue({
            el: '#' + name + 'Option',
            data: {
                message: "message" + this.name,
                collection: []
            },
            methods: {
                getElements: function (urlGetAll) {
                    var that = this;
                    $.ajax({
                        url: urlGetAll,
                        method: 'GET',
                        success: function (response) {
                            that.collection = response;
                        },
                        error: function () {
                            console.log("Impossible to load " + that.urlGetAll);
                        }
                    });
                }
            }
        });
    }

    populate() {
        this.app.getElements(this.urlGetAll);
    }
}

var supportGroups = new option('SupportGroup');
supportGroups.populate();   

var ReferenceTypes = new option('ReferenceType');
ReferenceTypes.populate();

var HeardOfUsFroms = new option('HeardOfUsFrom');
HeardOfUsFroms.populate();
