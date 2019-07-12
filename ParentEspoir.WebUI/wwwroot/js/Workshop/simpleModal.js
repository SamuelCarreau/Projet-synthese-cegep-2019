Vue.component('success-message-modal', {
    props: ['id'],
    template: `<div class="modal fade" :id="id" role="alertdialog">
                  <div class="modal-dialog modal-dialog-centered" role="alert">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title"><i class="far fa-check-circle h1" style="color: green;"></i></h5>
                        <button class="close" data-dismiss="modal">&times;</button>
                      </div>
                      <div class="modal-body">
                        <slot></slot>
                      </div>
                      <div class="modal-footer">
                        <button class="btn btn-outline-success" data-dismiss="modal">Fermer</button>
                      </div>
                    </div>
                  </div>
                </div>`
})

Vue.component('error-message-modal', {
    props: ['id'],
    template: `<div class="modal fade" :id="id" role="alertdialog">
                  <div class="modal-dialog modal-dialog-centered" role="alert">
                    <div class="modal-content">
                      <div class="modal-header">
                        <h5 class="modal-title"><i class="fas fa-times h1" style="color: red;"></i></h5>
                        <button class="close" data-dismiss="modal">&times;</button>
                      </div>
                      <div class="modal-body">
                        <slot></slot>
                      </div>
                      <div class="modal-footer">
                        <button class="btn btn-outline-danger" data-dismiss="modal">Fermer</button>
                      </div>
                    </div>
                  </div>
                </div>`
})