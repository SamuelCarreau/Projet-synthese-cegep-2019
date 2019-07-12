var app = new Vue({
    el: '#app',
    data: {
        isLoading: false,
        isWorkshopMembersCardVisible: false,
        isSeanceMembersCardVisible: false,
        isSeanceSelected: false,
        isSeanceCardVisible: false,
        isWorkshopSelected: false,
        isWorkshopCardVisible: false,
        isMembersCardsLarge: false,
        sessions: Object,
        userCanManageSession: false,
        selectedSessionId: null,
        selectedSessionStartDate: '',
        selectedSessionWorkshopList: [],
        selectedWorkshopId: null,
        selectedWorkshopDetails: {
            workshopId: '',
            workshopName: '',
            workshopTypeId: '',
            workshopTypeName: '',
            startDate: '',
            endDate: '',
            isOpen: '',
            workshopDescription: '',
        },
        selectedWorkshopSeanceList: { seances: [], seancesDuration: '' },
        selectedWorkshop: {
            workshopId: '',
            workshopName: '',
            workshopTypeId: '',
            workshopTypeName: '',
            startDate: '',
            endDate: '',
            isOpen: '',
            workshopDescription: '',
            seancesDuration: '',
            seances: [],
        },
        selectedSeance: {
            participants: [],
            seanceId: '',
            seanceDate: '',
            seanceName: '',
            seanceTimeSpan: '',
            seanceDescription: '',
            workshopId: ''
        },
        workshopTypes: [],
        selectedWorkshopMembers: [],
        selectedSeanceMembers: [],
        selectedSessionString: ''
    },
    methods: {
        updateSelectedSession: function (sessionId) {
            app.isLoading = true
            $.ajax({
                url: '/api/Workshop/GetAll/' + sessionId,
                method: 'GET',
                success: function (response) {
                    app.selectedSessionWorkshopList = response;
                },
                complete: function () {
                    app.isLoading = false
                }
            });
            app.selectedSessionId = sessionId;
            app.getSelectedSessionData(sessionId);
            app.isWorkshopSelected = false;
            app.isWorkshopCardVisible = true;
            app.isSeanceSelected = false;
            app.isSeanceCardVisible = false;
            app.isWorkshopMembersCardVisible = false;
            app.isSeanceMembersCardVisible = false;
        },
        getSessions: function () {

            app.isLoading = true
            $.ajax({
                url: '/api/Session/GetAll',
                method: 'GET',
                success: function (response) {
                    app.sessions = app.sortSession(response.sessions);
                    app.userCanManageSession = response.userCanManageSession;
                },
                complete: function () {
                    app.isLoading = false
                }

            });

        },
        sortSession: function (response) {

            var yearSessions = [];

            response.forEach(function (element) {

                index = app.yearExist(yearSessions, element.year)

                if (index == -1) {
                    var yearSession = { year: element.year, items: [] }
                    yearSession.items.push(element)
                    yearSessions.push(yearSession)
                }
                else {
                    yearSessions[index].items.push(element)
                }
            });

            return yearSessions
        },
        yearExist: function (yearSession, year) {
            for (i = 0; i < yearSession.length; i++) {
                if (yearSession[i].year == year)
                    return i
            }
            return -1
        },
        getWorkshopTypes: function () {
            app.isLoading = true
            $.ajax({
                url: '/api/WorkshopTypeApi/GetAll',
                method: 'GET',
                success: function (response) {
                    app.workshopTypes = response;
                },
                complete: function () {
                    app.isLoading = false
                }
            });
        },
        updateSelectedSeanceParticipants: function (participants) {
            app.participants = participants;
        },
        initSeanceMemberCard: function (seanceMembers) {
            this.$refs.seanceMemberCard.init(seanceMembers);
        },
        getSelectedSessionData(sessionId) {
            // to create the string with this format : 'Hivers 2019'
            for (var i = 0; i < app.sessions.length; i++) {
                for (var j = 0; j < app.sessions[i].items.length; j++) {
                    if (app.sessions[i].items[j].sessionId == sessionId) {
                        app.selectedSessionString = app.sessions[i].items[j].seasonName.toString() + ' ' + app.sessions[i].year.toString();
                        app.selectedSessionStartDate = app.sessions[i].items[j].startDate;
                    }
                }
            }
        },
        isNullOrEmpty(item) {
            return item === null || item === "";
        },
        // to check if an object is empty:
        isEmpty(obj) {
            for (var key in obj) {
                if (obj.hasOwnProperty(key))
                    return false;
            }
            return true;
        }
    }
})

// Init data on page load:

app.getSessions();
app.getWorkshopTypes();
