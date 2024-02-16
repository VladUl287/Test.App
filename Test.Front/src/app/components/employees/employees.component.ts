import { Component, OnInit } from "@angular/core"
import { Employee } from "../../types/employee"
import { EmployeeService } from "../../services/employee.service"
import { CommonModule } from "@angular/common"
import { EmployeeFilter } from "../../types/employeeFilter"
import { ModalComponent } from "../modal/employee.modal.component"
import { NgbModal } from "@ng-bootstrap/ng-bootstrap"

@Component({
    selector: 'app-chat',
    standalone: true,
    imports: [
        CommonModule,
        ModalComponent
    ],
    templateUrl: './employees.component.html',
    styleUrl: './employees.component.css',
})
export class EmployeesComponent implements OnInit {
    employees: Array<Employee> = []
    filters: EmployeeFilter = {
        salary: '',
        fullName: '',
        department: ''
    }

    constructor(
        private readonly employeeService: EmployeeService,
        private readonly ngbModal: NgbModal) { }

    ngOnInit(): void {
        this.getEmployees()
    }

    getEmployees(): void {
        this.employeeService.getEmployees(this.filters)
            .subscribe(result => {
                this.employees = result
            })
    }

    removeEmployee(id: string): void {
        this.employeeService.removeEmployee(id)
            .subscribe(() => {
                this.getEmployees()
            })
    }

    updateEmployee(employee: Employee): void {
        const modal = this.ngbModal.open(ModalComponent)
        const component = modal.componentInstance as ModalComponent
        component.action = 'Обновить'
        component.employeeForm.setValue({
            id: employee.id,
            salary: employee.salary,
            fullName: employee.fullName,
            departmentId: employee.department.id.toString(),
            dateBirth: this.getDateTime(employee.dateBirth),
            dateEmployment: this.getDateTime(employee.dateEmployment),
        })
        modal.closed.subscribe((values) => {
            this.employeeService.updateEmployee({
                ...values,
                department: {
                    id: values.departmentId
                }
            }).subscribe(() => this.getEmployees())
        })
    }

    createEmployee(): void {
        const modal = this.ngbModal.open(ModalComponent)
        modal.closed.subscribe((values) => {
            this.employeeService.createEmployee({
                ...values,
                department: {
                    id: values.departmentId
                }
            }).subscribe(() => this.getEmployees())
        })
    }

    sortBy(path: string): void {
        this.employees = this.employees.sort((a, b) => {
            if (this.getValue(a, path) > this.getValue(b, path)) return 1
            else if (this.getValue(a, path) < this.getValue(b, path)) return -1
            return 0
        })
    }

    getValue<ObjectType>(object: ObjectType, path: string) {
        const keys = path.split('.')
        let result: any = object
        for (const key of keys) {
            result = result[key as keyof ObjectType]
        }
        return result
    }

    private getDateTime(oldDate: string): string {
        const date = new Date(oldDate)
        let month = '' + (date.getMonth() + 1)
        let day = '' + date.getDate()
        if (month.length < 2) {
            month = '0' + month
        }
        if (day.length < 2) {
            day = '0' + day
        }
        return [date.getFullYear(), month, day].join('-')
    }

    inputChange(property: keyof EmployeeFilter, event: Event): void {
        this.filters[property] = (event.target as HTMLInputElement).value
        this.getEmployees()
    }
}