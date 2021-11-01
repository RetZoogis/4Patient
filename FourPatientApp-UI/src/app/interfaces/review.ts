import { Time } from "@angular/common";
import { Nursing } from "./nursing";
import { Accommodation } from "./accommodation";
import { Covid } from "./covid";
import { Cleanliness } from "./cleanliness";
import { Patient } from "./patient";

export interface Review {
    id: number,
    comfort: number,
    datePosted: Date, 
    message: string,
    hospitalId: number,
    patientId: number,
    nursing: Nursing,
    accommodation: Accommodation,
    covid : Covid,
    cleanliness: Cleanliness ,
    patient: Patient


}