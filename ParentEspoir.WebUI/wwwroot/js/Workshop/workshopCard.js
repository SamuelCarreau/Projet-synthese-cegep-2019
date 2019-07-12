Vue.component('workshop-details', {
    props: ['selectedWorkshopSeanceList', 'selectedWorkshopDetails'],
    computed: {
        startDate() {
            return this.selectedWorkshopDetails.startDate.slice(0, -9);
        },
        endDate() {
            return this.selectedWorkshopDetails.endDate.slice(0, -9);
        },
        seancesCount() {
            if (this.selectedWorkshopSeanceList !== null) {
                return this.selectedWorkshopSeanceList.seances.length;
            }
        },
        totalDuration() {
            if (this.selectedWorkshopSeanceList !== null) {
                return this.selectedWorkshopSeanceList.seancesDuration.slice(0, -3);
            }
        },
        isOpen() {
            return (this.selectedWorkshopDetails.isOpen) ? 'Ouvert' : 'Fermé'
        }
    },
    template: `
                <div class="details pr-1 mb-2">
                    <p class="text-center" >{{'Cette atelier à ' + seancesCount + " séances d'une durée totale de " + totalDuration + " heures."}}</p>
                    <dl class="row mb-0">
                        <dt class="col-sm-4">
                            Nom:
                        </dt>
                        <dd class="col-sm-8">
                            {{ selectedWorkshopDetails.workshopName }}
                        </dd>
                        <dt class="col-sm-4">
                            Volet:
                        </dt>
                        <dd class="col-sm-8">
                            {{ selectedWorkshopDetails.workshopTypeName }}
                        </dd>
                        <dt class="col-sm-4">
                            Début:
                        </dt>
                        <dd class="col-sm-8">
                            {{ startDate }}
                        </dd>   
                        <dt class="col-sm-4">
                            Fin:
                        </dt>
                        <dd class="col-sm-8">
                            {{ endDate }}
                        </dd> 
                    </dl>
                    <p class="mb-1">Cette atelier est un atelier <strong>{{isOpen}}.</strong></p>
                    <dl class="row">
                        <dt class="col-sm-12">
                            Description:
                        </dt>
                        <dd class="col-sm-12">
                            {{ selectedWorkshopDetails.workshopDescription }}
                        </dd>
                    </dl>
                </div>
                `
})

Vue.component('create-workshop-modal', {
    props: ['workshopTypes', 'sessionId', 'sessionStartDate'],
    data() {
        return {
            workshop: {
                workshopName: null,
                startDate: null,
                endDate: null,
                workshopDescription: null,
                seanceCount: null,
                isOpen: null,
                seanceLenght: null,
                dateTimeFirstSeance: null,
                intervalNbDays: null,
                sessionId: this.sessionId,
                workshopTypeId: null
            },
            seanceLengthHour: null,
            seanceLengthMinute: null,
            validationErrorMessages: {
                WorkshopName: '',
                StartDate: '',
                EndDate: '',
                WorkshopDescription: '',
                SeanceCount: '',
                SeanceLenght: '',
                DateTimeFirstSeance: '',
                IntervalNbDays: '',
                WorkshopTypeId: '',
                IsOpen: ''
            }
        }
    },
    watch: {
       sessionId(val) {
            this.workshop.sessionId = val;
        }
    },
    methods: {
        initCreateModal() {
            this.emptyValidationMessage();
            this.clearData();
            this.workshop.startDate = this.sessionStartDate.slice(0, -9);
        },
        changeMinEndDate() {
            $('form#createWorkshopForm input[name=EndDate]')
                .attr({ "min": $('form#createWorkshopForm input[name=StartDate]').val() });
        },
        onCreate() {
            app.isLoading = true
            if (!app.isNullOrEmpty(this.seanceLengthHour) && !app.isNullOrEmpty(this.seanceLengthMinute)) {
                this.workshop.seanceLenght = this.seanceLengthHour + ':' + this.seanceLengthMinute + ':00';
            }
            var that = this;
            $.ajax({
                dataType: 'json',
                contentType: 'application/json',
                url: '/api/Workshop/Create',
                method: 'POST',
                data: JSON.stringify(this.workshop),
                success() {
                    $('#createWorkshopModal').modal('toggle');
                    $('#createWorkshopSuccess').modal('toggle');
                    that.$emit('updateWorkshopList');
                },
                error(response) {
                    that.emptyValidationMessage();
                    if (response.responseJSON.errors !== undefined) {
                        var errors = response.responseJSON.errors;
                        for (var field in errors) { 
                            that.validationErrorMessages[field] = errors[field][0];
                            $('form#createWorkshopForm input[name=' + field + ']').css("border-color", "#dc3545");
                            $('form#createWorkshopForm textarea[name=' + field + ']').css("border-color", "#dc3545");
                        }
                    }
                },
                complete() {
                    app.isLoading = false;
                }
            });
        },
        emptyValidationMessage() {
            for (var field in this.validationErrorMessages) {
                this.validationErrorMessages[field] = '';
                $('form#createWorkshopForm input[name=' + field + ']').css("border-color", "");
                $('form#createWorkshopForm textarea[name=' + field + ']').css("border-color", "");
            }
        },
        clearData() {
            for (var field in this.workshop) {
                if (field == 'sessionId') {
                    this.workshop[field] = this.sessionId;
                } else {
                    this.workshop[field] = null;
                }
            }
        }
    },
    template: `<div class="modal fade" id="createWorkshopModal" tabindex="-1" role="alertdialog" aria-labelledby="updateModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-lg modal-dialog-centered" role="alert">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Création d'un nouvel atelier</h5>
                                <button class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="container">
                                    <form action="/" method="put" @submit.prevent="" id="createWorkshopForm">
                                        <div class="form-row align-items-center my-2">
                                            <label class="col-sm-3" style="font-weight:bold;">Nom: </label>
                                            <input class="form-control col-sm-9" style="" type="text" name="WorkshopName" v-model="workshop.workshopName"  />
                                            <div class="col-sm-3"></div>
                                            <span class="text-danger col-sm-9">{{validationErrorMessages.WorkshopName}}</span>
                                        </div>
                                        <p></p>
                                        <div class="form-row align-items-center my-2">
                                            <label class="col-md-3" style="font-weight:bold;">Volet: </label>
                                            <div class="d-block">
                                                <div class="">
                                                    <div class="form-check-inline" v-for="item in workshopTypes">
                                                        <label class="form-check-label">
                                                            <input type="radio" class="form-check-input" name="WorkshopType" :value="item.id" v-model="workshop.workshopTypeId">{{item.name}}
                                                        </label>
                                                    </div>
                                                </div>
                                            <span class="text-danger">{{validationErrorMessages.WorkshopTypeId}}</span>
                                            </div>
                                        </div>
                                        <div class="form-row align-items-center my-2">
                                            <label class="col-sm-6" style="font-weight:bold;">Début: </label>
                                            <input type="date" class="col-sm-6 form-control" name="StartDate" v-model="workshop.startDate" :min="sessionStartDate.slice(0, -9)" v-on:change="changeMinEndDate()"/>
                                            <div class="col-sm-6"></div>
                                            <span class="text-danger col-sm-6">{{validationErrorMessages.StartDate}}</span>
                                        </div>
                                        <div class="form-row align-items-center my-2">
                                            <label class="col-sm-6" style="font-weight:bold;">Fin: </label>
                                            <input type="date" class="col-sm-6 form-control" name="EndDate" v-model="workshop.endDate" :min="sessionStartDate.slice(0, -9)" />
                                            <div class="col-sm-6"></div>
                                            <span class="text-danger col-sm-6">{{validationErrorMessages.EndDate}}</span>     
                                        </div>
                                        <div class="form-row align-items-center my-2">
                                            <div class="col-sm-6"></div>
                                            <div class="col-sm-6">
                                                <div class="form-check">
                                                    <input class="form-check-input" type="radio" value="true" v-model="workshop.isOpen">
                                                    <label class="form-check-label">Atelier Ouvert</label>
                                                </div>
                                                <div class="form-check">
                                                    <input class="form-check-input" type="radio" value="false" v-model="workshop.isOpen">
                                                    <label class="form-check-label">Atelier Fermé</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6"></div>
                                            <span class="text-danger col-sm-6">{{validationErrorMessages.IsOpen}}</span>
                                        </div>
                                        <div class="form-row my-2">
                                            <label class="col-sm-12" style="font-weight:bold;">Description: </label>
                                            <textarea style="resize: none" rows="3" class="form-control col-sm-12" type="text" name="WorkshopDescription" v-model="workshop.workshopDescription"></textarea>
                                            <span class="text-danger">{{validationErrorMessages.WorkshopDescription}}</span>
                                        </div>
                                        <p />
                                        <button class="btn btn-outline-primary" data-toggle="collapse" data-target="#defaultSeanceData">Créer des séances par défault</button>
                                        <p />
                                        <div id="defaultSeanceData" class="collapse">
                                            <div class="form-row align-items-center my-2">
                                                <label class="col-sm-10" style="font-weight:bold;">Nombre de séance: </label>
                                                
                                                <input class="form-control col-sm-2" type="number" min="0" name="SeanceCount" v-model="workshop.seanceCount"  />
                                                <div class="col-sm-6"></div>
                                                <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceCount}}</span>     
                                            </div>  
                                            <div class="form-row align-items-center my-2">
                                                <label class="col-sm-6" style="font-weight:bold;">Durée d'une séance (en heure): </label>
                                                <div class="input-group col-sm-6 px-0">
                                                    <div class="input-group-prepend">
                                                        <span class="input-group-text" id="">Heures &amp; Minutes</span>
                                                    </div>
                                                    <input name="SeanceLenght" class="form-control" type="number" placeholder="Heures" min="0" v-model="seanceLengthHour">
                                                    <input name="SeanceLenght" class="form-control" type="number" placeholder="Minutes" min="0" max="59" v-model="seanceLengthMinute">
                                                </div>
                                                <div class="col-sm-6"></div>
                                                <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceLenght}}</span> 
                                            </div>
                                            <div class="form-row align-items-center my-2">
                                                <label class="col-sm-6" style="font-weight:bold;">Date de la première séance: </label>
                                                <input type="date" class="col-sm-6 form-control" name="DateTimeFirstSeance" v-model="workshop.dateTimeFirstSeance" />
                                                <div class="col-sm-6"></div>
                                                <span class="text-danger col-sm-6">{{validationErrorMessages.DateTimeFirstSeance}}</span> 
                                            </div>
                                            <div class="form-row align-items-center my-2">
                                                <label class="col-sm-10" style="font-weight:bold;">Interval en jours entre chaque séance: </label>
                                                <input class="form-control col-sm-2" type="number" min="0" name="IntervalNbDays" v-model="workshop.intervalNbDays"  />
                                                <div class="col-sm-6"></div>
                                                <span class="text-danger col-sm-6">{{validationErrorMessages.IntervalNbDays}}</span> 
                                            </div>
                                        </div> 
                                    </form>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <a href="#" class="btn btn-outline-primary btn-block mr-1" @click="onCreate()">Ajouter cette atelier</a>
                                        </div>
                                        <div class="col-sm-6">
                                            <a href="#" class="btn btn-outline-danger btn-block ml-1" data-dismiss="modal">Annuler</a>
                                        </div>                                    
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>`
})

Vue.component('update-workshop-modal', {
    props: ['workshopTypes', 'selectedWorkshopDetails', 'sessionId', 'sessionStartDate'],
    data() {
        return {
            updatedWorkshop: {},
            validationErrorMessages: {
                WorkshopName: '',
                StartDate: '',
                EndDate: '',
                WorkshopDescription: '',
                WorkshopTypeId: '',
                IsOpen: ''
            }
        }
    },
    watch: {
        sessionId(val) {
            this.updatedWorkshop.sessionId = val;
        }
    },
    methods: {
        prepareUpdateModal() {
            this.updatedWorkshop = Object.assign({}, this.selectedWorkshopDetails);
            this.updatedWorkshop.startDate = this.updatedWorkshop.startDate.slice(0, -9);
            this.updatedWorkshop.endDate = this.updatedWorkshop.endDate.slice(0, -9);
            this.updatedWorkshop.sessionId = this.sessionId;
            this.updatedWorkshop.workshopTypeId = this.findWorkshopTypeId(this.selectedWorkshopDetails.workshopTypeName);
            this.emptyValidationMessage();
        },
        findWorkshopTypeId(name) {
            for (var i = 0; i < this.workshopTypes.length; i++) {
                if (this.workshopTypes[i].name === name) {
                    return this.workshopTypes[i].id;
                }
            }
        },
        changeMinEndDate() {
            $('form#updateWorkshopForm input[name=EndDate]')
                .attr({ "min": $('form#updateWorkshopForm input[name=StartDate]').val() });
        },
        onUpdate() {    
            app.isLoading = true
            var that = this;
            $.ajax({
                dataType: 'json',
                contentType: 'application/json',
                url: '/api/Workshop/Update',
                method: 'PUT',
                data: JSON.stringify(this.updatedWorkshop),
                success() {
                    $('#updateWorkshopModal').modal('toggle');
                    $('#createWorkshopSuccess').modal('toggle');
                    that.$emit('updateSelectedWorkshop', that.updatedWorkshop.workshopId);
                    that.$emit('updateWorkshopList');
                },
                error(response) {
                    that.emptyValidationMessage();
                    if (response.responseJSON.errors !== undefined) {
                        var errors = response.responseJSON.errors;
                        for (var field in errors) {
                            that.validationErrorMessages[field] = errors[field][0];
                            $('form#updateWorkshopForm input[name=' + field + ']').css("border-color", "#dc3545");
                            $('form#updateWorkshopForm textarea[name=' + field + ']').css("border-color", "#dc3545");
                        }
                    }
                },
                complete() {
                    app.isLoading = false
                }
            });
        },
        emptyValidationMessage() {
            for (var field in this.validationErrorMessages) {
                this.validationErrorMessages[field] = '';
                $('form#updateWorkshopForm input[name=' + field + ']').css("border-color", "");
                $('form#updateWorkshopForm textarea[name=' + field + ']').css("border-color", "");
            }
        }
    },
    template: `<div class="modal fade" id="updateWorkshopModal" tabindex="-1" role="alertdialog" aria-labelledby="updateModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-centered" role="alert" >
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="deleteModalLabel">Modifier cette atelier</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <form id="updateWorkshopForm" action="/" method="put" @submit.prevent="">
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-3" style="font-weight:bold;">Nom: </label>
                                    <input class="form-control col-sm-9" type="text" name="WorkshopName" v-model="updatedWorkshop.workshopName" />
                                    <div class="col-sm-3"></div>
                                    <span class="text-danger col-sm-9">{{validationErrorMessages.WorkshopName}}</span>
                                </div>
                                <p></p>
                                <div class="form-row align-items-center my-2">
                                    <label class="col-md-3" style="font-weight:bold;">Volet: </label>
                                    <div class="d-block">
                                        <div class="">
                                            <div class="form-check-inline" v-for="item in workshopTypes">
                                                <label class="form-check-label">
                                                    <input type="radio" class="form-check-input" name="WorkshopType" :value="item.id" v-model="updatedWorkshop.workshopTypeId" :checked="item.id == updatedWorkshop.workshopTypeId">{{item.name}}
                                                </label>
                                            </div>
                                        </div>
                                    <span class="text-danger">{{validationErrorMessages.WorkshopTypeId}}</span>
                                    </div>
                                </div>
                               
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Début: </label>
                                    <input type="date" class="col-sm-6 form-control" name="StartDate" v-model="updatedWorkshop.startDate" :min="sessionStartDate.slice(0, -9)" v-on:change="changeMinEndDate()"/>
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.StartDate}}</span>
                                </div>
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Fin: </label>
                                    <input type="date" class="col-sm-6 form-control" name="EndDate" v-model="updatedWorkshop.endDate" :min="updatedWorkshop.startDate" />
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.EndDate}}</span>     
                                </div>
                                <div class="form-row align-items-center my-2">
                                    <div class="col-sm-6"></div>
                                    <div class="col-sm-6">
                                        <div class="form-check">
                                            <input class="form-check-input" type="radio" value="true" v-model="updatedWorkshop.isOpen">
                                            <label class="form-check-label">Atelier Ouvert</label>
                                        </div>
                                        <div class="form-check">
                                            <input class="form-check-input" type="radio" value="false" v-model="updatedWorkshop.isOpen">
                                            <label class="form-check-label">Atelier Fermé</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.IsOpen}}</span>
                                </div>
                                <div class="form-row my-2">
                                    <label class="col-sm-12" style="font-weight:bold;">Description: </label>
                                    <textarea style="resize: none" rows="6" class="form-control col-sm-12" type="text" name="WorkshopDescription" v-model="updatedWorkshop.workshopDescription"></textarea>
                                    <span class="text-danger">{{validationErrorMessages.WorkshopDescription}}</span>
                                </div>
                            </form>
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-outline-primary" @click="onUpdate()">Modifier</button>
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Annuler</button>
                        </div>
                    </div>
                </div>
               </div>`
})

Vue.component('delete-workshop-modal', {
    props: ['workshopName', 'workshopId'],
    data() {
        return {
            validationErrorMessages: ''
        }
    },
    methods: {
        prepareDeleteModal() {
            this.validationErrorMessages = '';
            if (!$('#modalAlertDeleteWorkshop').hasClass('d-none')) {
                this.toggle('#modalAlertDeleteWorkshop');
            }
        },
        onDelete() {
            app.isLoading = true
            var id = this.workshopId;
            var that = this;
            $.ajax({
                url: '/api/Workshop/Delete/' + id,
                method: 'DELETE',
                success() {
                    $('#deleteWorkshopModal').modal('toggle');
                    $('#deleteWorkshopSuccess').modal('toggle');
                    that.$emit('updateWorkshopList');
                    that.$emit('removeWorkshopSelection');
                },
                error(response) {
                    var errors = response.responseJSON;
                    that.validationErrorMessages = errors.errors[0]['errorMessage'];    
                    if ($('#modalAlertDeleteWorkshop').hasClass('d-none')) {
                        that.toggle('#modalAlertDeleteWorkshop');
                    }
                },
                complete() {
                    app.isLoading = false
                }
            });
        },
        toggle(id) {
            $(id).toggleClass('d-none')
        }
    },
    template: `<div class="modal fade" id="deleteWorkshopModal" tabindex="-1" role="alertdialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="alert">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="deleteModalLabel">Confirmation de la suppression</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div id="modalAlertDeleteWorkshop" class="alert alert-danger d-none" role="alert">
                              {{validationErrorMessages}}
                              <button class="close" type="button" v-on:click="toggle('#modalAlertDeleteWorkshop')">
                                <span aria-hidden="true">&times;</span>
                              </button>
                            </div>
                            Êtes-vous sûr de vouloir désactiver l'atelier {{workshopName}} ?
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-danger" @click="onDelete()">Désactiver</button>
                            <button type="button" class="btn btn-outline-primary" data-dismiss="modal">Annuler</button>
                        </div>
                    </div>

                </div>
               </div>`
})

Vue.component('workshop-card', {
    props: ['selectedSessionWorkshopList',
        'selectedWorkshopDetails',
        'selectedWorkshopSeanceList', 
        'sessionId',
        'sessionStartDate',
        'workshopTypes',
        'isWorkshopSelected',
        'selectedSessionString',
        'userCanManageSession'],
    methods: {
        updateSelectedWorkshop: function (workshopId) {
            app.isLoading = true;
            app.selectedWorkshopId = workshopId;

            var workshopdone = false;
            var participantdone = false;
            app.isWorkshopSelected = true;
            app.isSeanceCardVisible = true;
            app.isWorkshopMembersCardVisible = true;
            app.isSeanceMembersCardVisible = false;
            app.isSeanceSelected = false;


            // TODO
            // put this in a method 
            for (var i = 0; i < app.selectedSessionWorkshopList.length; i++) {
                if (app.selectedSessionWorkshopList[i].workshopId == workshopId) {
                    app.selectedWorkshopDetails = app.selectedSessionWorkshopList[i];
                }
            }

            $.ajax({
                url: '/api/Seance/GetAll/' + workshopId,
                method: 'GET',
                success: function (response) {
                    app.selectedWorkshopSeanceList = response;
                },
                complete() {
                    workshopdone = true;
                    app.isLoading = !(workshopdone && participantdone)
                }
            });

            $.ajax({
                url: '/api/Participant/GetAll/' + workshopId,
                method: 'GET',
                success: function (response) {
                    app.selectedWorkshopMembers = response;
                },
                complete() {
                    participantdone = true; 
                    app.isLoading = !(workshopdone && participantdone)
                }
            });
        },
        updateWorkshopList() {
            $.ajax({
                url: '/api/Workshop/GetAll/' + app.selectedSessionId,
                method: 'GET',
                success: function (response) {
                    app.selectedSessionWorkshopList = response;
                },
                error() {
                    // display error modal
                }
            });
        },
        removeWorkshopSelection() {
            app.isWorkshopSelected = false;
            app.isSeanceCardVisible = false;
            app.isWorkshopMembersCardVisible = false;
            app.isSeanceMembersCardVisible = false;
            app.isSeanceSelected = false;
        },
        prepareCreateModal() {
            this.$refs.createWorkshopModal.initCreateModal();
        },
        prepareUpdateModal() {
            this.$refs.updateWorkshopModal.prepareUpdateModal();
        },
        prepareDeleteModal() {
            this.$refs.deleteWorkshopModal.prepareDeleteModal();
        }
    },
    template: `<div class="card p-3">
                    <div class="form-row align-items-center">
                        <h4 class="col-8 m-0">Ateliers: </h4>
                        <a href="#" class="btn btn-outline-primary col-4" v-on:click="prepareCreateModal()" data-toggle="modal" data-target="#createWorkshopModal">Ajouter</a>
                    </div>
                    {{ selectedSessionString }}
                    <div v-if="userCanManageSession">
                       <a href="/Volets" style="color:green;">Gestion des volets</a>
                    </div>
                    <hr />
                    <p class="text-center" v-if="!selectedSessionWorkshopList.length">Il n'y a aucun atelier dans cette session</p>
                    <div v-if="selectedSessionWorkshopList.length" class="scrolling-name-list list-group">
                        <a href="#" v-for="workshop in selectedSessionWorkshopList" :key="workshop.workshopId" v-on:click="updateSelectedWorkshop(workshop.workshopId)" :class="{active: workshop.workshopId == selectedWorkshopDetails.workshopId && isWorkshopSelected}" class="list-group-item list-group-item-action list-group-item-light">{{workshop.workshopName}}</a>
                    </div>
                    <div v-if="isWorkshopSelected">
                        <hr />
                        <workshop-details :selected-workshop-seance-list="selectedWorkshopSeanceList" :selected-workshop-details="selectedWorkshopDetails" :workshop-types="workshopTypes"></workshop-details>
                    </div>
                    <div v-if="isWorkshopSelected" class="row mt-auto">
                        <div class="col-sm-6"> 
                            <button type="submit" class="btn btn-outline-primary btn-block mr-1" data-toggle="modal" data-target="#updateWorkshopModal" v-on:click="prepareUpdateModal()"><i class="fas fa-wrench h5"></i></button>
                        </div>
                        <div class="col-sm-6">
                            <button class="btn btn-outline-danger btn-block ml-1" data-toggle="modal" data-target="#deleteWorkshopModal" v-on:click="prepareDeleteModal()"><i class="fas fa-trash-alt h5"></i></button>
                        </div>
                    </div>

                    <create-workshop-modal :workshop-types="workshopTypes" :session-id="sessionId" :session-start-date="sessionStartDate" v-on:updateWorkshopList="updateWorkshopList()" ref="createWorkshopModal"></create-workshop-modal>
                    <success-message-modal id="createWorkshopSuccess">La création de cette atelier c'est déroulé avec succès.</success-message-modal>

                    <update-workshop-modal :selected-workshop-details="selectedWorkshopDetails" :session-id="sessionId" :session-start-date="sessionStartDate" :workshop-types="workshopTypes" ref="updateWorkshopModal" v-on:updateWorkshopList="updateWorkshopList()" v-on:updateSelectedWorkshop="(id) => updateSelectedWorkshop(id)"></update-workshop-modal>
                    <success-message-modal id="updateWorkshopSuccess">La modification de cette atelier c'est déroulé avec succès.</success-message-modal>

                    <delete-workshop-modal :workshop-name="selectedWorkshopDetails.workshopName" :workshop-id="selectedWorkshopDetails.workshopId" v-on:updateWorkshopList="updateWorkshopList()" ref="deleteWorkshopModal" v-on:removeWorkshopSelection="removeWorkshopSelection()"></delete-workshop-modal>
                    <success-message-modal id="deleteWorkshopSuccess">La désactivation de cette atelier c'est déroulé avec succès.</success-message-modal>
               </div>`
})