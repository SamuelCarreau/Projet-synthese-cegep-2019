Vue.component('create-seance-modal', {
    props: ['workshopId', 'workshopName', 'selectedWorkshopDetails'],
    data() {
        return {
            seance: {
                seanceDate: null,
                seanceName: null,
                seanceTimeSpan: null,
                seanceDescription: null,
                workshopId: this.workshopId,
            },
            seanceTime: null,
            seanceTimeSpanHour: null,
            seanceTimeSpanMinute: null,
            validationErrorMessages: {
                SeanceName: '',
                SeanceDate: '',
                SeanceTimeSpan: '',
                SeanceDescription: ''
            }
        }
    },
    watch: {
        workshopId(val) {
            this.seance.workshopId = val;
        }
    },
    methods: {
        initCreateModal() {
            this.emptyValidationMessage();
            this.clearData();
        },
        onCreate() {
            app.isLoading = true
            if (!app.isNullOrEmpty(this.seanceTimeSpanHour) && !app.isNullOrEmpty(this.seanceTimeSpanMinute)) {
                this.seance.seanceTimeSpan = this.seanceTimeSpanHour + ':' + this.seanceTimeSpanMinute + ':00';
            }
            if (!app.isNullOrEmpty(this.seance.seanceDate) && !app.isNullOrEmpty(this.seanceTime)) {
                this.seance.seanceDate = this.seance.seanceDate + 'T' + this.seanceTime;
            }
            let newSeance = JSON.stringify(this.seance);
            var that = this;
            $.ajax({
                dataType: 'json',
                contentType: 'application/json',
                url: '/api/Seance/Create',
                method: 'POST',
                data: newSeance,
                success() {
                    $('#createSeanceModal').modal('toggle');
                    $('#createSeanceSuccess').modal('toggle');
                    that.$emit('updateSeanceList');
                },
                error(response) {
                    that.emptyValidationMessage();
                    if (response.responseJSON.errors !== undefined) {
                        var errors = response.responseJSON.errors;
                        for (var field in errors) {
                            that.validationErrorMessages[field] = errors[field][0];
                            $('form#createSeanceForm input[name=' + field + ']').css("border-color", "#dc3545");
                            $('form#createSeanceForm textarea[name=' + field + ']').css("border-color", "#dc3545");
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
                $('form#createSeanceForm input[name=' + field + ']').css("border-color", "");
                $('form#createSeanceForm textarea[name=' + field + ']').css("border-color", "");
            }
        },
        clearData() {
            for (var field in this.seance) {
                if (field == 'workshopId') {
                    this.seance[field] = this.workshopId;
                } else {
                    this.seance[field] = null;
                }
            }
            this.seanceTime = null;
            this.seanceTimeSpanHour = null;
            this.seanceTimeSpanMinute = null;
        }
    },
    template: `<div class="modal fade" id="createSeanceModal" tabindex="-1" role="alertdialog" aria-labelledby="updateModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-centered" role="alert" >
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="deleteModalLabel">Ajouter une séance dans {{workshopName}} </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                           <form id="createSeanceForm" action="/" method="put" @submit.prevent="">
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-3" style="font-weight:bold;">Nom: </label>
                                    <input class="form-control col-sm-9" type="text" name="SeanceName" v-model="seance.seanceName" />
                                    <div class="col-sm-3"></div>
                                    <span class="text-danger col-sm-9">{{validationErrorMessages.SeanceName}}</span>
                                </div>
                                <p></p>
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Date: </label>
                                    <input type="date" class="col-sm-6 form-control" name="SeanceDate" v-model="seance.seanceDate" :min="selectedWorkshopDetails.startDate.slice(0, -9)" :max="selectedWorkshopDetails.endDate.slice(0, -9)"/>
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceDate}}</span>
                                </div>
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Heure de la séance: </label>
                                    <input type="time" class="col-sm-6 form-control" name="seanceTime" v-model="seanceTime" />
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceTime}}</span>
                                </div>
                                 <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Durée d'une séance: </label>
                                    <div class="input-group col-sm-6 px-0 mt-1">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text">Heures &amp; Minutes</span>
                                        </div>
                                        <input name="SeanceTimeSpan" class="form-control" type="number" placeholder="Heures" min="0" v-model="seanceTimeSpanHour">
                                        <input name="SeanceTimeSpan" class="form-control" type="number" placeholder="Minutes" min="0" max="59" v-model="seanceTimeSpanMinute">
                                    </div>
                                        
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceTimeSpan}}</span>
                                </div>
                                <div class="form-row my-2">
                                    <label class="col-sm-12" style="font-weight:bold;">Description: </label>
                                    <textarea style="resize: none" rows="6" class="form-control col-sm-12" type="text" name="SeanceDescription" v-model="seance.seanceDescription"></textarea>
                                    <span class="text-danger">{{validationErrorMessages.SeanceDescription}}</span>
                                </div>
                           </form>
                        </div>
                            <div class="modal-footer">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <a href="#" class="btn btn-outline-primary btn-block mr-1" @click="onCreate()">Ajouter cette séance</a>
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

Vue.component('update-seance-modal', {
    props: ['selectedSeance', 'workshopId'],
    data() {
        return {
            updatedSeance: {
                seanceId: null,
                seanceDate: null,
                seanceName: null,
                seanceTimeSpan: null,
                seanceDescription: null,
                workshopId: this.workshopId
            },
            seanceTime: null,
            seanceTimeSpanHour: null,
            seanceTimeSpanMinute: null,
            validationErrorMessages: {
                SeanceName: '',
                SeanceDate: '',
                SeanceTimeSpan: '',
                SeanceDescription: ''
            }
        }
    },
    watch: {
        workshopId(val) {
            this.updatedSeance.workshopId = val;
        }
    },
    methods: {
        prepareUpdateModal() {
            this.updatedSeance = Object.assign({}, this.selectedSeance); // this create a shallow copy
            this.seanceTime = this.updatedSeance.seanceDate.slice(11, 19);
            this.updatedSeance.seanceDate = this.updatedSeance.seanceDate.slice(0, -9);
            this.seanceTimeSpanHour = this.updatedSeance.seanceTimeSpan.slice(0, 2);
            this.seanceTimeSpanMinute = this.updatedSeance.seanceTimeSpan.slice(3, 5);
            this.emptyValidationMessage();
        },
        onUpdate() {
            app.isLoading = true;
            if (!app.isNullOrEmpty(this.seanceTimeSpanHour) && !app.isNullOrEmpty(this.seanceTimeSpanMinute)) {
                this.updatedSeance.seanceTimeSpan = this.seanceTimeSpanHour + ':' + this.seanceTimeSpanMinute + ':00';
            } else {
                this.updatedSeance.seanceTimeSpan = null;
            }
            if (!app.isNullOrEmpty(this.updatedSeance.seanceDate) && !app.isNullOrEmpty(this.seanceTime)) {
                this.updatedSeance.seanceDate = this.updatedSeance.seanceDate + 'T' + this.seanceTime;
            }
            let updatedData = JSON.stringify(this.updatedSeance);
            var that = this;
            $.ajax({
                dataType: 'json',
                contentType: 'application/json',
                url: '/api/Seance/Update',
                method: 'PUT',
                data: updatedData,
                success() {
                    $('#updateSeanceModal').modal('toggle');
                    $('#updateSeanceSuccess').modal('toggle');
                    that.$emit('updateSelectedSeance', that.updatedSeance.seanceId);
                    that.$emit('updateSeanceList');
                },
                error(response) {
                    that.emptyValidationMessage();
                    if (response.responseJSON.errors !== undefined) {
                        var errors = response.responseJSON.errors;
                        console.log(errors);
                        for (var field in errors) {
                            that.validationErrorMessages[field] = errors[field][0];
                            $('form#updateSeanceForm input[name=' + field + ']').css("border-color", "#dc3545");
                            $('form#updateSeanceForm textarea[name=' + field + ']').css("border-color", "#dc3545");
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
                $('form#updateSeanceForm input[name=' + field + ']').css("border-color", "");
                $('form#updateSeanceForm textarea[name=' + field + ']').css("border-color", "");
            }
        }
    },
    template: `<div class="modal fade" id="updateSeanceModal" tabindex="-1" role="alertdialog" aria-labelledby="updateModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg modal-dialog-centered" role="alert" >
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="deleteModalLabel">Modifier cette séance</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                           <form id="updateSeanceForm" action="/" method="put" @submit.prevent="">
                                <div class="form-row align-items-center my-2 form-group">
                                    <label class="col-sm-3" style="font-weight:bold;">Nom: </label>
                                    <input class="form-control col-sm-9" type="text" name="SeanceName" v-model="updatedSeance.seanceName" />
                                    <div class="col-sm-3"></div>
                                    <span class="text-danger col-sm-9">{{validationErrorMessages.SeanceName}}</span>
                                </div>
                                <p></p>
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Date: </label>
                                    <input type="date" class="col-sm-6 form-control" name="SeanceDate" v-model="updatedSeance.seanceDate" />
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceDate}}</span>
                                </div>
                                <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Heure de la séance: </label>
                                    <input type="time" class="col-sm-6 form-control" name="seanceTime" v-model="seanceTime" />
                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceTime}}</span>
                                </div>
                                 <div class="form-row align-items-center my-2">
                                    <label class="col-sm-6" style="font-weight:bold;">Durée d'une séance: </label>
                                    
                                    <div class="input-group col-sm-6">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text" id="">Heures &amp; Minutes</span>
                                        </div>
                                        <input name="SeanceTimeSpan" class="form-control" type="number" placeholder="Heures" min="0" v-model="seanceTimeSpanHour">
                                        <input name="SeanceTimeSpan" class="form-control" type="number" placeholder="Minutes" min="0" max="59" v-model="seanceTimeSpanMinute">
                                    </div>

                                    <div class="col-sm-6"></div>
                                    <span class="text-danger col-sm-6">{{validationErrorMessages.SeanceTimeSpan}}</span>
                                </div>
                                <div class="form-row my-2">
                                    <label class="col-sm-12" style="font-weight:bold;">Description: </label>
                                    <textarea style="resize: none" rows="6" class="form-control col-sm-12" type="text" name="SeanceDescription" v-model="updatedSeance.seanceDescription"></textarea>
                                    <span class="text-danger">{{validationErrorMessages.SeanceDescription}}</span>
                                </div>
                           </form>
                        </div>
                            <div class="modal-footer">
                                <button type="submit" class="btn btn-danger" @click="onUpdate()">Modifier</button>
                                <button type="button" class="btn btn-outline-primary" data-dismiss="modal">Annuler</button>
                            </div>
                        </div>
                    </div>
               </div>`
})

Vue.component('delete-seance-modal', {
    props: ['seanceName', 'seanceId', 'workshopId'],
    data() {
        return {
            validationErrorMessages: ''
        }
    },
    methods: {
        prepareDeleteModal() {
            this.validationErrorMessages = '';
            if (!$('#modalAlertDeleteSeance').hasClass('d-none')) {
                this.toggle('#modalAlertDeleteSeance');
            }
        },
        onDelete() {
            app.isLoading = true;
            var deleteSeanceModel = {
                SeanceId: this.seanceId,
                WorkshopId: this.workshopId
            };
            var that = this;
            $.ajax({
                dataType: 'json',
                contentType: 'application/json',
                url: '/api/Seance/Delete',
                method: 'DELETE',
                data: JSON.stringify(deleteSeanceModel),
                success() {
                    $('#deleteSeanceModal').modal('toggle');
                    $('#deleteSeanceSuccess').modal('toggle');
                    that.$emit('updateSeanceList');
                    that.$emit('removeSeanceSelection');
                },
                error(response) {
                    var errors = response.responseJSON.errors;
                    that.validationErrorMessages = errors.SeanceId[0];
                    if ($('#modalAlertDeleteSeance').hasClass('d-none')) {
                        that.toggle('#modalAlertDeleteSeance');
                    }
                },
                complete() {
                    app.isLoading = false;
                }
            });
        },
        toggle(id) {
            $(id).toggleClass('d-none')
        }
    },
    template: `<div class="modal fade" id="deleteSeanceModal" tabindex="-1" role="alertdialog" aria-labelledby="deleteModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="alert">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="deleteModalLabel">Confirmation de la désactivation</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div id="modalAlertDeleteSeance" class="alert alert-danger d-none" role="alert">
                              {{validationErrorMessages}}
                              <button class="close" type="button" v-on:click="toggle('#modalAlertDeleteSeance')">
                                <span aria-hidden="true">&times;</span>
                              </button>
                            </div>
                            Êtes-vous sûr de vouloir désactiver l'atelier {{seanceName}} ?
                        </div>
                        <div class="modal-footer">
                            <button type="submit" class="btn btn-danger" @click="onDelete()">Désactiver</button>
                            <button type="button" class="btn btn-outline-primary" data-dismiss="modal">Annuler</button>
                        </div>
                    </div>
                </div>
               </div>`
})

Vue.component('seance-details', {
    props: ['selectedSeance'],
    computed: {
        time() {
            return this.selectedSeance.seanceDate.slice(11, 16);
        },
        date() {
            return this.selectedSeance.seanceDate.slice(0, -9);
        },
        seanceTimeSpan() {
            return this.selectedSeance.seanceTimeSpan.slice(0, -3);
        }
    },
    template: `
                <div class="details">
                    <dl class="row">
                        <dt class="col-sm-6">
                            Nom:
                        </dt>
                        <dd class="col-sm-6">
                            {{ selectedSeance.seanceName }}
                        </dd>
                        <dt class="col-sm-6">
                            Date:
                        </dt>
                        <dd class="col-sm-6">
                            {{ date }}
                        </dd>
                        <dt class="col-sm-6">
                            Heure de la séance:
                        </dt>
                        <dd class="col-sm-6">
                            {{ time }}
                        </dd>
                        <dt class="col-sm-6">
                            Durée d'une séance:
                        </dt>
                        <dd class="col-sm-6">
                            {{ seanceTimeSpan }} heures
                        </dd>
                        </dd>  
                        <dt class="col-sm-12">
                            Description:
                        </dt>
                        <dd class="col-sm-12">
                            {{ selectedSeance.seanceDescription }}
                        </dd>
                    </dl>
                </div>`
})

Vue.component('seance-card', {
    props: ['selectedSeance', 'isSeanceSelected', 'selectedWorkshopSeanceList', 'selectedWorkshopDetails'],
    methods: {
        updateSelectedSeance: function (id) {
            app.isSeanceMembersCardVisible = true;
            app.isWorkshopMembersCardVisible = false;
            app.isSeanceSelected = true;
            app.isLoading = true
            that = this;
            $.ajax({
                url: '/api/Seance/Get/' + id,
                method: 'GET',
                success: function (response) {
                    app.selectedSeance = response;
                    app.selectedSeanceMembers = response.participants;
                    app.initSeanceMemberCard(response.participants);
                },
                complete() {
                    app.isLoading = false
                }
            });
        },
        removeSeanceSelection: function () {
            app.isSeanceMembersCardVisible = false;
            app.isWorkshopMembersCardVisible = true;
            app.isSeanceSelected = false;
        },
        updateSeanceList() {
            app.isLoading = true
            var that = this;
            $.ajax({
                url: '/api/Seance/GetAll/' + that.selectedWorkshopDetails.workshopId,
                method: 'GET',
                success: function (response) {
                    app.selectedWorkshopSeanceList = response;
                },
                complete() {
                    app.isLoading = false;
                }
            });
        },
        prepareCreateModal() {
            this.$refs.createSeanceModal.initCreateModal();

        },
        prepareUpdateModal() {
            this.$refs.updateModal.prepareUpdateModal();
        },
        prepareDeleteModal() {
            this.$refs.deleteSeanceModal.prepareDeleteModal();
        }
    },
    template: ` <div class="card p-3">
                    <div class="form-row align-items-center">
                        <h4 class="col-8 m-0">Séances pour: </h4>
                        <a href="#" class="btn btn-outline-primary col-4" data-toggle="modal"  v-on:click="prepareCreateModal()" data-target="#createSeanceModal">Ajouter</a>
                    </div>
                    {{selectedWorkshopDetails.workshopName}}
                    <hr />
                    <p class="text-center" v-if="!selectedWorkshopSeanceList.seances.length">Il n'y a aucune séance dans cette atelier</p>
                    <div v-if="selectedWorkshopSeanceList.seances.length" class="scrolling-name-list list-group">
                        <a href="#" v-for="seance in selectedWorkshopSeanceList.seances" :key="seance.seanceId" v-on:click="updateSelectedSeance(seance.seanceId)" :class="{active: seance.seanceId == selectedSeance.seanceId && isSeanceSelected}" class="list-group-item list-group-item-action list-group-item-light">{{ seance.seanceName }}</a>
                    </div>
                    <div v-if="isSeanceSelected">
                        <hr />
                        <seance-details :selected-seance="selectedSeance"></seance-details>
                    </div>
                    <div class="row mt-auto" v-if="isSeanceSelected">
                        <div class="col-sm-6">
                            <button type="submit" class="btn btn-outline-primary btn-block mr-1" data-toggle="modal" data-target="#updateSeanceModal" v-on:click="prepareUpdateModal()"><i class="fas fa-wrench h5"></i></button>
                        </div>
                        <div class="col-sm-6">
                            <button class="btn btn-outline-danger btn-block ml-1" data-toggle="modal" data-target="#deleteSeanceModal" v-on:click="prepareDeleteModal()"><i class="fas fa-trash-alt h5"></i></button>
                        </div>
                    </div>

                    <create-seance-modal
                        ref="createSeanceModal"
                        :selected-workshop-details="selectedWorkshopDetails"
                        :workshop-name="selectedWorkshopDetails.workshopName" 
                        :workshop-id="selectedWorkshopDetails.workshopId" 
                        v-on:updateSeanceList="updateSeanceList()"></create-seance-modal>
                    <success-message-modal id="createSeanceSuccess">La création de cette atelier c'est déroulé avec succès.</success-message-modal>

                    <update-seance-modal :selected-seance="selectedSeance" :workshop-id="selectedWorkshopDetails.workshopId" ref="updateModal" v-on:updateSeanceList="updateSeanceList()" v-on:updateSelectedSeance="(id) => updateSelectedSeance(id)"></update-seance-modal>
                    <success-message-modal id="updateSeanceSuccess">La modification de cette séance c'est déroulé avec succès.</success-message-modal>

                    <delete-seance-modal :seance-name="selectedSeance.seanceName" :seance-id="selectedSeance.seanceId" ref="deleteSeanceModal" :workshop-id="selectedSeance.workshopId" v-on:updateSeanceList="updateSeanceList()" v-on:removeSeanceSelection="removeSeanceSelection()"></delete-seance-modal>
                    <success-message-modal id="deleteSeanceSuccess">La désactivation de cette séance c'est déroulé avec succès.</success-message-modal>
                </div>`
})
