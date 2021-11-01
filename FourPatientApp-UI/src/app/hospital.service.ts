import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Hospital } from './interfaces/hospital';
import { environment as env } from '../environments/environment';

@Injectable({
    providedIn: 'root'
  })

  export class HospitalService {
    readonly APIUrl=`${env.apiUrl}`;
  
    constructor(private https:HttpClient) { }

    ListHospital():Observable<Hospital[]>{
      return this.https.get<Hospital[]>(this.APIUrl+'/Hospital');
    }

    GetHospitalbyId(id : number){
      
      return this.https.get<Hospital>(this.APIUrl+'/Hospital/'+ id)
    }

    SearchHospitals(str: string){
      return this.https.get<Hospital>(this.APIUrl+'/Hospital/search/'+ str)
    }

    // httpOptions = {
    //   headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    // };
    // EditHospital-For Later
    // updateHero(hero: Hero): Observable<any> {
    //   return this.http.put(this.heroesUrl, hero, this.httpOptions).pipe(
    //     tap(_ => this.log(`updated hero id=${hero.id}`)),
    //     catchError(this.handleError<any>('updateHero'))
    //   );
  }