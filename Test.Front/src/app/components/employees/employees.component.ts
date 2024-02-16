import { ChangeDetectionStrategy, Component, OnInit } from "@angular/core"
import { Employee } from "../../types/employee"
import { EmployeeService } from "../../services/employee.service"
import { CommonModule } from "@angular/common"

@Component({
    selector: 'app-chat',
    standalone: true,
    imports: [
        CommonModule
    ],
    templateUrl: './employees.component.html',
    // changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployeesComponent implements OnInit {
    employees: Array<Employee> = []

    constructor(private readonly employeeService: EmployeeService) { }

    ngOnInit(): void {
        this.getEmployees()
    }

    getEmployees(): void {
        this.employeeService.getEmployees()
            .subscribe(result => {
                this.employees = result
            })
    }
}