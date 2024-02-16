import { Department } from "./department"

export type Employee = {
    id: string,
    fullName: string,
    dateBirth: Date,
    dateEmployment: Date,
    salary: string,
    department: Department
}