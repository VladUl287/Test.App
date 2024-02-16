import { Department } from "./department"

export type Employee = {
    id: string,
    fullName: string,
    dateBirth: string,
    dateEmployment: string,
    salary: string,
    department: Department
}