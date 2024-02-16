import { Observable } from "rxjs"
import { Injectable } from "@angular/core"
import { HttpClient } from "@angular/common/http"
import { Employee } from './../types/employee'
import { environment } from "../../environments/environment"

@Injectable({
    providedIn: 'root',
})
export class EmployeeService {
    private readonly apiUrl = environment.apiUrl

    constructor(private readonly http: HttpClient) { }

    getEmployees(): Observable<Employee[]> {
        return this.http.get<Employee[]>(`${this.apiUrl}/employee/getall`)
    }
}