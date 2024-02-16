import { DepartmentService } from './../../services/departament.service';
import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core"
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms"
import { NgbActiveModal } from "@ng-bootstrap/ng-bootstrap"
import { Department as Departament } from '../../types/department';

@Component({
    selector: 'app-modal',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule
    ],
    templateUrl: './employee.modal.component.html',
})
export class ModalComponent implements OnInit {
    action: 'Создать' | 'Обновить' = 'Создать'
    departaments: Array<Departament> = []

    constructor(
        public readonly activeModal: NgbActiveModal,
        private readonly departamentService: DepartmentService) { }

    ngOnInit() {
        this.departamentService.getDepartaments()
            .subscribe(result => {
                this.departaments = result
            })
    }

    employeeForm = new FormGroup({
        id: new FormControl(''),
        fullName: new FormControl('', Validators.required),
        salary: new FormControl('', Validators.required),
        dateBirth: new FormControl('', Validators.required),
        dateEmployment: new FormControl('', Validators.required),
        departmentId: new FormControl('', Validators.required),
    })

    onSubmit() {
        if (this.employeeForm.invalid) {
            return
        }
        this.activeModal.close(this.employeeForm.value)
    }
}