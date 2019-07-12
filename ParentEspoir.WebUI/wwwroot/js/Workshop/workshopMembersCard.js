Vue.component('workshop-members-manager-modal', {
    props: ['selectedWorkshopDetails', 'selectedWorkshopId', 'currentMembers', 'selectedSessionString'],
    data: function () {
        return {
            search: '',
            allAvailableMembers: [],
            searchedMembers: []
        }
    },
    methods: {
        FilterMembers: function () {
            this.searchedMembers = [];
            for (var i = 0; i < this.allAvailableMembers.length; i++) {
                if (this.allAvailableMembers[i].name.toLowerCase().includes(this.search.toLowerCase())) {
                    this.searchedMembers.push(this.allAvailableMembers[i]);
                }
            }
        },
        AddMembers: function (idCustomer) {
            var that = this;
            $.ajax({
                url: '/api/Participant/Create',
                method: 'POST',
                data: JSON.stringify({ CustomerId: idCustomer, WorkshopId: that.selectedWorkshopId }),
                contentType: "application/json",
                success: function () {
                    for (var i = 0; i < that.allAvailableMembers.length; i++) {
                        if (that.allAvailableMembers[i].customerId === idCustomer) {
                            var member = that.allAvailableMembers.splice(i, 1)[0];
                            that.currentMembers.push(member);
                            break;
                        }
                    }
                    for (var i = 0; i < that.searchedMembers.length; i++) {
                        if (that.searchedMembers[i].customerId === idCustomer) {
                            var member = that.searchedMembers.splice(i, 1)[0];
                            break;
                        }
                    }
                    that.$emit('UpdateParticipantList');
                }
            })
        },
        SubstractMembers: function (idCustomer) {
            var that = this;
            $.ajax({
                url: '/api/Participant/Delete',
                method: 'DELETE',
                data: JSON.stringify({ CustomerId: idCustomer, WorkshopId: that.selectedWorkshopId }),
                contentType: "application/json",
                success: function () {
                    for (var i = 0; i < that.currentMembers.length; i++) {
                        if (that.currentMembers[i].customerId === idCustomer) {
                            var member = that.currentMembers.splice(i, 1)[0];
                            that.allAvailableMembers.push(member);
                            that.searchedMembers.push(member);
                            break;
                        }
                    }
                    that.$emit('UpdateParticipantList');
                }
            })
        },
        GetAllAvailableMembers: function () {
            var that = this;
            $.ajax({
                url: '/api/Participant/GetAllNotInWorkshop/' + that.selectedWorkshopId,
                method: 'GET',
                success: function (response) {
                    that.allAvailableMembers = response;
                    that.FilterMembers();
                }
            });
        }
    },
    template: `<div class="modal fade" id="modalAddMembers">
                    <div class="modal-dialog modal-lg modal-dialog-centered">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Transférer clients vers l'Atelier: {{ selectedWorkshopDetails.workshopName }} {{ selectedSessionString }}</h5>
                                <button class="close" data-dismiss="modal">&times;</button>
                            </div>
                                 <div class="modal-body">
                                         <div class="col-lg-12 col-lg-offset-2 centered">
                                           <h5 class="col-12">Clients disponibles</h5>
                                           <div class="input-group mb-3 col-12">
                                               <div class="input-group-prepend">
                                                   <button id="searchButton" v-on:click="FilterMembers()" class="btn btn-outline-primary" type="button" >Rechercher</button>
                                               </div>
                                               <input id="searchInput" v-on:keyup.enter="FilterMembers()" class="form-control mr-3" v-model="search" type="text" placeholder="Rechercher par nom...">
                                               <h5 class="col-6 float-right">Clients assignés</h5>
                                           </div>
                                           <hr />
                                             <div class="col-md-6 float-left" >
                                                 <div style="overflow-y: scroll; height: 400px;">
                                                 <ul v-if="searchedMembers.length !== 0" href="#" class="list-group">
                                                     <li class="list-group-item list-group-item-action list-group-item-light" v-for="item in searchedMembers"  v-bind:id="item.customerId">
                                                         {{item.name}}
                                                     <a href="#" v-on:click="AddMembers(item.customerId)" class="float-right btn btn-outline-primary col-sm-2"><i class="fas fa-plus"></i></a></li>
                                                     </ul>
                                                 <p v-else>Aucun client n'a été trouvé</p>
                                                 </div>
                                             </div>
                                             <div class="col-md-6 float-left">
                                                 <div style="overflow-y: scroll; height: 400px;">
                                                 <ul href="#" class="list-group">
                                                     <li class="list-group-item list-group-item-action list-group-item-light" v-for="item in currentMembers" v-bind:id="item.customerId">
                                                         {{item.name}}
                                                     <a href="#"v-on:click="SubstractMembers(item.customerId)" class="float-right btn btn-outline-primary col-sm-2"><i class="fas fa-minus"></i></a></li>
                                                     </ul>
                                                 </div>
                                             </div>
                                       </div>
                                 </div>
                                 <div class="modal-footer"> 
                            </div>
                        </div>
                    </div>
                </div>`
})

Vue.component("workshop-members-card", {
    props: ['selectedWorkshopDetails', 'selectedWorkshopId', 'members', 'selectedSessionString'],
    methods: {
        prepareModal () {
            this.$refs.addMembersModal.GetAllAvailableMembers();
        },
        UpdateParticipantList() {
            var that = this;
            $.ajax({
                url: '/api/Participant/GetAll/' + that.selectedWorkshopId,
                method: 'GET',
                success: function (response) {
                    app.selectedWorkshopMembers = response;
                }
            });
        }
    },
    template:
        `<div class="card p-3">
            <h4>Participants aux ateliers:</h4>
            <p>{{ selectedSessionString }}</p>
            <div v-if="!members.length">
                <hr />
                <p class="text-center">Il n'y a aucun participant dans cette atelier</p>
            </div>
            <div class="details" v-if="members.length">
                <table class="table">
                    <thead>
                        <th>Nom</th>
                        <th>Heure d'absence cumulée</th>
                   </thead class="table col-sm-6">
                   <tbody v-for="item in members">
                        <td>{{item.name}}</td>
                        <td>{{item.nbHourLate.slice(0, -3)}} heures</td>
                    </tbody>
                </table>
            </div>
            <div class="mt-auto">
                <a href="#" class="btn btn-outline-primary btn-block mt-auto" data-toggle="modal" data-target="#modalAddMembers"  v-on:click="prepareModal()"><i class="fas fa-user-plus" style="font-size:x-large"></i></a>
            </div>
            <workshop-members-manager-modal v-on:UpdateParticipantList="UpdateParticipantList()" ref="addMembersModal" :selected-workshop-details="selectedWorkshopDetails" :selected-session-string="selectedSessionString" :selected-workshop-id="selectedWorkshopId" :current-members="members" ></workshop-members-manager-modal>
        </div>`
})