import { Observable } from "rxjs"
import { Injectable } from "@angular/core"
import { HttpClient, HttpParams } from "@angular/common/http"
import { environment } from "../../environments/environment"
import { Department } from "../types/department"

@Injectable({
    providedIn: 'root',
})
export class DepartmentService {
    private readonly apiUrl = environment.apiUrl

    constructor(private readonly http: HttpClient) { }

    getDepartaments(): Observable<Department[]> {
        return this.http.get<Department[]>(`${this.apiUrl}/departament/getall`)
    }
}