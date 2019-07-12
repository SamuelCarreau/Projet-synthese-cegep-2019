Vue.component("seance-members-card", {
    props: ['selectedSeance'],
    data: function () {
        return {
            ParticipationStatus: [{ name: "À l'heure", id: 0 }, { name: "Absent", id: 1 }, { name: "Retard", id: 2 }],
            participantsAttendance: {}
        }
    },
    computed: {
        nbHourSeance() {
            var hour = this.selectedSeance.seanceTimeSpan.slice(0, 2).toString();
            if (hour[0] === '0') {
                return hour[1];
            }
            return hour;
        },
        nbMinuteSeance() {
            var minute = this.selectedSeance.seanceTimeSpan.slice(3, 5).toString();
            if (minute[0] === '0') {
                return minute[1];
            }
            return minute;
        }
    },
    updated() {
        for (var id in this.participantsAttendance) {
            this.onParticipantStatusChange(id);
        }
    },
    methods: {
        updateHourValue(id, event) {
            this.participantsAttendance[id].NbHourLate = event.target.value;
        },
        updateMinuteValue(id, event) {
            this.participantsAttendance[id].NbMinuteLate = event.target.value;
        },
        init: function (seanceMembers) {
            this.participantsAttendance = {};
            for (i = 0; i < seanceMembers.length; i++) {
                var hour = seanceMembers[i].nbHourLate.slice(0, 2).toString();
                if (hour[0] === '0') {
                    hour = hour[1];
                }
                var minute = seanceMembers[i].nbHourLate.slice(3, 5).toString();
                if (minute[0] === '0') {
                    minute = minute[1];
                }

                this.participantsAttendance[seanceMembers[i].particiantId] = {
                    ParticipationStatus: seanceMembers[i].participationStatus,
                    NbHourLate: hour,
                    NbMinuteLate: minute,
                    CustomerName: seanceMembers[i].customerName,
                    temporaryTimeValue: { hour: hour, minute: minute }
                };
            }
        },
        onParticipantStatusChange: function (participantId) {
            
            if (this.participantsAttendance[participantId].ParticipationStatus == 0) {
                $('#hour-' + participantId).val('0').prop('disabled', true);
                $('#minute-' + participantId).val('0').prop('disabled', true);
                this.participantsAttendance[participantId].NbHourLate = '0';
                this.participantsAttendance[participantId].NbMinuteLate = '0';
            } else if (this.participantsAttendance[participantId].ParticipationStatus == 1) {
                $('#hour-' + participantId).val(this.nbHourSeance).prop('disabled', true);
                $('#minute-' + participantId).val(this.nbMinuteSeance).prop('disabled', true);
                this.participantsAttendance[participantId].NbHourLate = this.nbHourSeance;
                this.participantsAttendance[participantId].NbMinuteLate = this.nbMinuteSeance;
            } else {
                $('#hour-' + participantId).val(this.participantsAttendance[participantId].temporaryTimeValue.hour).prop('disabled', false);
                $('#minute-' + participantId).val(this.participantsAttendance[participantId].temporaryTimeValue.minute).prop('disabled', false);
                this.participantsAttendance[participantId].NbHourLate = this.participantsAttendance[participantId].temporaryTimeValue.hour;
                this.participantsAttendance[participantId].NbMinuteLate = this.participantsAttendance[participantId].temporaryTimeValue.minute;
            }
        },
        SendAttendance: function () {
            var participants = { SeanceId: this.selectedSeance.seanceId, WorkshopId: this.selectedSeance.workshopId };
            participants['ParticipantsAttendance'] = [];
            for (participantId in this.participantsAttendance) {
                participants['ParticipantsAttendance'].push({
                    ParticipantId: participantId,
                    ParticipationStatus: this.participantsAttendance[participantId].ParticipationStatus,
                    NbHourLate: this.participantsAttendance[participantId].NbHourLate,
                    NbMinuteLate: this.participantsAttendance[participantId].NbMinuteLate
                });
            }

            app.isLoading = true;

            $.ajax({
                url: '/api/Participant/Update',
                method: 'PUT',
                data: JSON.stringify(participants),
                contentType: "application/json",
                success() {
                    $('#sendAttendanceSuccess').modal('toggle');
                },
                complete() {
                    app.isLoading = false;
                }
            })
        },
        isEmpty(obj) {
            return app.isEmpty(obj);
        }
    },
    template: `
    <div class="card p-3">
        <h4>Participants à la séance</h4>
        <p>{{selectedSeance.seanceName}}</p>
        <div v-if="isEmpty(participantsAttendance)">
            <hr />
            <p class="text-center">Il n'y a aucun participant dans cette séance</p>
        </div>
        <div class="details" v-if="!isEmpty(participantsAttendance)">
            <table class="table col-sm-12 details">
                    <col width="25%">
                    <col width="30%">
                    <col width="45%">
                <thead>
                    <th>Nom</th>
                    <th>Présence</th>
                    <th>Retard</th>
            </thead>
                 <tbody v-for="(member, key) in participantsAttendance">

                   <td>{{member.CustomerName}}</td>

                   <td> 
                    <div class="form-row align-items-center my-2 p-0">
                        <form v-on:change="onParticipantStatusChange(key)">
                            <div class="row">
                                <div class="form-check-inline col-12" v-for="status in ParticipationStatus">
                                <label class="form-check-label">
                                <input type="radio"
                                        class="form-check-input text-align-center"
                                        name="Status" v-model="member.ParticipationStatus"
                                        :value="status.id"
                                        :checked="status.id == member.ParticipationStatus"
                                        >{{status.name}}
                                </label>
                                </div>
                            </div>
                        </form>
                      </div> 
                    </td>
                
                    <td>
                        <div class="form-row align-items-center my-2 p-0">
                        <input :id="'hour-' + key" class="form-control col-sm-6 " type="number" min="0" v-model="member.temporaryTimeValue.hour" v-on:change="updateHourValue(key, $event)"/>
                        <label class="col-sm-6">Heure(s) </label>
                        <input :id="'minute-' + key" class="form-control col-sm-6" type="number" max="59" min="0" v-model="member.temporaryTimeValue.minute" v-on:change="updateMinuteValue(key, $event)"/>
                        <label class="col-sm-6">Minute(s) </label>
                        </div> 
                    </td>

                </tbody>
            </table>
        </div>
        <button type="submit" class= "btn btn-outline-primary btn-block mt-auto" v-on:click="SendAttendance()"><i class="fas fa-save fa-2x"></i></button>
        <success-message-modal id="sendAttendanceSuccess">Les présences se sont envoyer avec succès!</success-message-modal>
    </div>
        `
})
