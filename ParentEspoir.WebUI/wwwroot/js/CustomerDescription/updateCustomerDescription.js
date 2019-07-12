$.ajax({
    url: '/api/CustomerApi/GetName/' + $('#customerId').val(),
    method: 'GET',
    contentType: "application/json",
    success: function (response) {
        $('span.customerName').text(response);
    }
});

class Option {
    constructor(name) {
        this.name = name;
        this.urlGetAll = '/api/' + name + '/GetAll';
        this.urlCreate = '/api/' + name + '/Create';
        this.app = new Vue({
            el: '#' + name + 'Option',
            data: {
                message: "message" + this.name,
                collection: [],
            },
            methods: {
                getElements: function (urlGetAll) {
                    var that = this;
                    $.ajax({
                        url: urlGetAll,
                        method: 'GET',
                        success: function (response) {
                            that.collection = response;
                        }
                    });
                }
            },
        });
    }

    populate() {
        this.app.getElements(this.urlGetAll);
    }
}

var AvailabilityOption = new Option('Availability');
AvailabilityOption.populate();

var CitizenStatusOption = new Option('CitizenStatus');
CitizenStatusOption.populate();

var FamilyTypeOption = new Option('FamilyType');
FamilyTypeOption.populate();

var HomeTypeOption = new Option('HomeType');
HomeTypeOption.populate();

var IncomeSourceOption = new Option('IncomeSource');
IncomeSourceOption.populate();

var LanguageOption = new Option('Language');
LanguageOption.populate();

var LegalCustodyOption = new Option('LegalCustody');
LegalCustodyOption.populate();

var MaritalStatusOption = new Option('MaritalStatus');
MaritalStatusOption.populate();

var ParentOption = new Option('Parent');
ParentOption.populate();

var SchoolingOption = new Option('Schooling');
SchoolingOption.populate();

var SexOption = new Option('Sex');
SexOption.populate();

var TransportTypeOption = new Option('TransportType');
TransportTypeOption.populate();

var YearlyIncomeOption = new Option('YearlyIncome');
YearlyIncomeOption.populate();

var SkillToDevelopOption = new Option('SkillToDevelop');
SkillToDevelopOption.populate();

var SocialServiceOption = new Option('SocialService');
SocialServiceOption.populate();

var ChildrenAgeBracketOption = new Option('ChildrenAgeBracket');
ChildrenAgeBracketOption.populate();


var IsPregnant = new Vue({
    el: '#IsPregnant',
    data: {
        isPregnant: $('div#IsPregnant select').children('option:selected').val()
    }
})

var HasPersonnelFollowUp = new Vue({
    el: '#HasPersonnelFollowUp',
    data: {
        hasPersonnelFollowUp: $('div#HasPersonnelFollowUp select').children('option:selected').val()
    }
})

var HasLegalCustody = new Vue({
    el: '#HasLegalCustody',
    data: {
        hasCustody: $('div#HasLegalCustody select').children('option:selected').val(),
    },
    methods: {
        toggelDisable: function () {
            if (this.hasCustody == 'true') {
                $('select#LegalCustodyId').attr('disabled', '');
            } else {
                $('select#LegalCustodyId').removeAttr('disabled');
            }
        }
    }
})

var PreferedDaysOption = new Vue({
    el: "#PreferedDaysOption",
    data: {
        selection: []
    },
    methods: {
        addSelection: function () {
            $('div#PreferedDaySelection').empty();
            this.selection.forEach(function (item) {
                $('div#PreferedDaySelection').append('<input id="PreferedDays" name="PreferedDays" type="hidden" value="' + item + '" />');
            });
        }
    }
})

var IsMember = new Vue({
    el: '#IsMember',
    data: {
        isMember: $('div#IsMember select').children('option:selected').val()
    }
})

let hasChange = false;

$("form :input").change(function () {
    hasChange = true;
});

window.onbeforeunload = function () {
    if (hasChange) {
        return "";
    }
}