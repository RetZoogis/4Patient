import { Injectable } from '@angular/core';
import { environment as env } from '../environments/environment';
import { Covid } from './interfaces/covid';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CovidService {
  readonly APIUrl=`${env.apiUrl}`;
  
  constructor(private https:HttpClient) { }

  GetCovidById(id: number){
    return this.https.get<Covid>(this.APIUrl+'/Covid/'+ id)
  }
}
