Vue.component('manage-session-modal', {
    props: ['sessions'],
    data() {
        return {
            actions: ['Ajouter', 'Enlever'],
            model: { Year: null, Season: null },
            SessionId: '',
            selectedAction: '',
            seasons: [],
            theSeasons: [{ Id: 1, Name: "Automne" }, { Id: 2, Name: "Hiver" }, { Id: 3, Name: "Printemps" }, { Id: 4, Name: "Été" }],
            validationErrorMessages: ''
        }
    },
    methods: {
        init() {
            this.model.Year = this.sessions[0].year
            this.onYearChange()
        },
        getSeasons() {
            var theYear = this.model.Year
            for (var i = 0; i < this.sessions.length; i++) {
                if (this.sessions[i].year == theYear)
                    return this.sessions[i].items
            }
            return null;
        },
        onActionChange() {
            switch (this.selectedAction) {
                case 'Ajouter': // Create
                    this.SessionId = null
                    this.model.Season = this.theSeasons[0].Id
                    this.seasons = null
                    break;

                case 'Enlever': // Delete
                    this.init()
                    break;

                default:
            }
        },
        onYearChange() {
            this.seasons = this.getSeasons()
            this.model.Season = this.seasons.seasonName
            this.SessionId = this.seasons[0].sessionId
        },
        onClickValidate() {
            //Add season
            if (this.selectedAction == this.actions[0]) {
                var that = this;
                app.isLoading = true
                $.ajax({
                    dataType: 'json',
                    contentType: 'application/json',
                    url: '/api/Session/Create',
                    method: 'POST',
                    data: JSON.stringify(that.model),
                    success() {
                        app.getSessions()
                        $('#manageSession').modal('hide')
                    },
                    error(response) {
                        var errors = response.responseJSON.errors
                        console.log(errors);
                        that.validationErrorMessages = errors.Name[0]
                        that.toggle('#modalAlert')
                    },
                    complete() {
                        app.isLoading = false
                    }

                });
            } // Del season
            else {
                app.isLoading = true
                var that = this;
                $.ajax({
                    dataType: 'json',
                    contentType: 'application/json',
                    url: '/api/Session/Delete/' + that.SessionId,
                    method: 'DELETE',
                    success() {
                        app.getSessions()
                        $('#manageSession').modal('hide')
                    },
                    error(response) {
                        var errors = response.responseJSON
                        that.validationErrorMessages = errors[0]
                        that.toggle('#modalAlert')
                    },
                    complete() {
                        app.isLoading = false
                    }
                });
            }
        },
        toggle(id) {
            $(id).toggleClass('d-none')
        }
    },
    template:
        `<div>
        <div class="modal" id="manageSession">
        <div class="modal-dialog">
            <div class="modal-content modal-top-60">

                <div class="modal-header">
                    <h5 class="modal-title">Gerer les sessions</h5>
                    <button class="close" data-dismiss="modal">&times;</button>
                </div>

                <div class="modal-body">
                    <div id="modalAlert" class="alert alert-danger d-none" role="alert">
                      {{validationErrorMessages}}
                      <button class="close" type="button" v-on:click="toggle('#modalAlert')">
                        <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                    <form @submit.prevent="">
                        <div class="input-group mb-2">
                            <div class="input-group-prepend">
                               <label class="input-group-text" id="">Action</label> 
                            </div>
                             <select v-on:change="onActionChange()" v-model="selectedAction" class="custom-select">
                              <option v-for="action in actions" :value="action">{{action}}</option>
                            </select> 
                        </div>
                            <input class="d-none" type="number" v-model="SessionId" >

                        <div v-if="selectedAction == actions[0]">
                            <div class="input-group mb-2">
                                <div class="input-group-prepend">
                                   <span class="input-group-text">Année</span> 
                                </div>
                                <input class="form-control" type="number" v-model="model.Year" >
                            </div>
                            <div class="input-group mb-2">
                                <div class="input-group-prepend">
                                   <label class="input-group-text">Saison</label> 
                                </div>
                                 <select v-model="model.Season" class="custom-select">
                                    <option v-for="s in theSeasons" :value="s.Id">{{s.Name}}</option>
                                </select> 
                            </div>
                            <button v-on:click="onClickValidate()" class="btn btn-primary btn-block">{{selectedAction}}</button>
                        </div>

                        <div v-else-if="selectedAction == actions[1]">
                            <div class="input-group mb-2">
                                <div class="input-group-prepend">
                                   <span class="input-group-text">Année</span> 
                                </div>
                                <select class="custom-select" v-model="model.Year" v-on:change="onYearChange">
                                    <option v-for="obj in sessions">{{obj.year}}</option>
                                </select>
                            </div>
                            <div class="input-group mb-2">
                                <div class="input-group-prepend">
                                   <label class="input-group-text">Saison</label> 
                                </div>
                                 <select class="custom-select" v-model="SessionId">
                                    <option v-for="item in seasons" :value="item.sessionId">{{item.seasonName}}</option>
                                </select> 
                            </div>
                            <button  v-on:click="onClickValidate()" class="btn btn-danger btn-block">{{selectedAction}}</button>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </div>
</div>`
})

Vue.component('side-bar', {
    props: ['sessions', 'userCanManageSession'],
    methods: {
        sidebarCollapse: function () {
            $('#sidebar').toggleClass('col-md-2')
            $('#sidebar').toggleClass('col-md-1')
            $('.components').toggleClass('d-none')
            $('#sidebar-header>h3:first').toggleClass('d-none')
            $('#sidebar-header>h3:first').toggleClass('d-inline')
            $('#sidebar-header>a:first').toggleClass('d-none')
            $('#sidebar-header>i:first').toggleClass('d-none')
            $('#toggler-icon').toggleClass('fa-rotate-180')
            app.isMembersCardsLarge = (app.isMembersCardsLarge) ? false : true
        },
        collapseAllPanelexeptId: function (id) {
            this.sessions.forEach(function (element) {
                $('#collapse-' + element.year).collapse("hide")
            });
            $('#collapse-' + id).collapse("show")
        },
        initManageModal: function () {
            this.$refs.manageSession.init()
        }
    },

    template: `<div id="sidebar" class="card col-md-2 p-0 d-flex align-items-start flex-column">

                <div id="sidebar-header" class="text-center w-100 p-2">
                    <h3 class="d-inline ml-2">Sessions </h3>
                    <a v-if="userCanManageSession" v-on:click="initManageModal()" class="btn-sidebar float-right mr-2 mt-1" data-toggle="modal" data-target="#manageSession" href="#" ><i class="fas fa-cog"></i></a>     
                    <i class="far fa-calendar-alt d-none"></i>
                </div>

                <div class="components w-100 slides" >
                    <div v-for="obj in sessions">
                        <a class="d-block p-2 btn-sidebar" 
                           data-toggle="collapse" 
                           href="#" 
                           aria-expanded="false"
                            v-on:click="collapseAllPanelexeptId(obj.year)"   
                        >{{ obj.year }}</a>
                        <ul :id="'collapse-' + obj.year" class="collapse list-unstyled">
                            <li v-for="item in obj.items" class=""> 
                                <a class="d-block pl-3" v-on:click="$emit('change-selected-session', item.sessionId)"
                                   href="#">
                                    {{ item.seasonName }}
                                </a> 
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="mt-auto text-center w-100">                                 
                    <a v-on:click="sidebarCollapse()" class="d-block text-center p-3 btn-sidebar" href="#">
                        <i id="toggler-icon" class="fas fa-angle-left"></i>
                    </a>
                </div>

                <!-- Modal -->
                <manage-session-modal ref="manageSession" :sessions="sessions"></manage-session-modal>
            </div>`
})