import { Observable } from "rxjs"
import { Injectable } from "@angular/core"
import { HttpClient, HttpParams } from "@angular/common/http"
import { Employee } from './../types/employee'
import { environment } from "../../environments/environment"
import { EmployeeFilter } from "../types/employeeFilter"

@Injectable({
    providedIn: 'root',
})
export class EmployeeService {
    private readonly apiUrl = environment.apiUrl

    constructor(private readonly http: HttpClient) { }

    getEmployees(filters: EmployeeFilter): Observable<Employee[]> {
        return this.http.get<Employee[]>(`${this.apiUrl}/employee/getall`, {
            params: this.getParams(filters)
        })
    }

    removeEmployee(id: string): Observable<void> {
        return this.http.delete<void>(`${this.apiUrl}/employee/delete/${id}`)
    }

    updateEmployee(employee: Employee): Observable<void> {
        return this.http.put<void>(`${this.apiUrl}/employee/update`, employee)
    }

    createEmployee(employee: Employee): Observable<void> {
        const body: Record<string, any> = {}
        for (const key in employee) {
            const value = employee[key as keyof EmployeeFilter]
            if (value) {
                body[key] = value
            }
        }
        return this.http.post<void>(`${this.apiUrl}/employee/create`, body)
    }

    private getParams(filters: EmployeeFilter) {
        let params = new HttpParams()
        for (const key in filters) {
            const value = filters[key as keyof EmployeeFilter]
            if (value) {
                params = params.append(key, value)
            }
        }
        return params
    }
}