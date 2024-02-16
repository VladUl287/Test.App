import { Routes } from '@angular/router'
import { CompanyComponent } from './components/company/company.component'
import { EmployeesComponent } from './components/employees/employees.component'

export const routes: Routes = [
    { path: 'company', component: CompanyComponent },
    { path: 'employees', component: EmployeesComponent },
]
