import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Review } from './interfaces/review';
import { Cleanliness } from './interfaces/cleanliness';
import { Nursing } from './interfaces/nursing';
import { Covid } from './interfaces/covid';
import { Accommodation } from './interfaces/accommodation';
import { environment as env } from '../environments/environment';

@Injectable({
    providedIn: 'root'
  })

  export class ReviewService {
    readonly APIUrl=`${env.apiUrl}`;
  
    constructor(private https:HttpClient) { }

    ListReviews():Observable<Review[]>{
      return this.https.get<Review[]>(this.APIUrl+'/Review');
    }

    GetReviewbyHospitalId(hospitalid : number) :Observable<Review[]>{
    
      return this.https.get<Review[]>(this.APIUrl+'/Review/'+ hospitalid)
    }
    
  
    ListCleanliness():Observable<Cleanliness[]>{
        return this.https.get<Cleanliness[]>(this.APIUrl+'/Cleanliness');
    }

    GetLatestId() : Observable<Review>{
      console.log(this.GetLatestId);
      return this.https.get<Review>(this.APIUrl+'/Review/select');
      
    }

    AddReview(review : Review): Observable<number>{
      return this.https.post<number>(this.APIUrl+'/Review/Create',review)
  }

    AddCleanliness(cleanliness : Cleanliness) : Observable<Cleanliness>{
        return this.https.post<Cleanliness>(this.APIUrl+'/Cleanliness/Create',cleanliness)

    }

    AddNursing(nursing : Nursing) : Observable<Nursing>{
      return this.https.post<Nursing>(this.APIUrl+'/Nursing/Create',nursing)

  }

  AddCovid(covid : Covid) : Observable<Covid>{
    return this.https.post<Covid>(this.APIUrl+'/Covid/Create',covid)

}

AddAccommodation(x : Accommodation) : Observable<Accommodation>{
  return this.https.post<Accommodation>(this.APIUrl+'/Accommodation/Create',x)

}


  }