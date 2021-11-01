import { Component, OnInit, Input } from '@angular/core';
import {FormBuilder, FormGroup, FormControl, Validators} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '@auth0/auth0-angular';
import { HospitalService } from '../hospital.service';
import { Hospital } from '../interfaces/hospital';
import { ReviewService } from '../review.service';


@Component({
  selector: 'app-create-review',
  templateUrl: './create-review.component.html',
  styleUrls: ['./create-review.component.css']
})
export class CreateReviewComponent implements OnInit {

  userId = -1;
  reviewId = -1;
  selected = 0;
  hospitals : Hospital[] | null=null;
  // hospitals: Hospital[] | null = null;

  reviewForm = new FormGroup({
    comfort: new FormControl(4.43),
    datePosted: new FormControl(new Date()),
    message: new FormControl(''),
    hospitalid: new FormControl(''),
    patientid: new FormControl('')
  });

  cleanlinessForm = new FormGroup({
    id: new FormControl(''),
    waitingroom: new FormControl(''),
    wardroom: new FormControl(''),
    equipment: new FormControl(''),
    bathroom: new FormControl(''),
    averageci: new FormControl('')
  });
  
  nursingForm = new FormGroup({
    id: new FormControl(''),
    attentiveness: new FormControl(''),
    transparency: new FormControl(''),
    knowledge: new FormControl(''),
    compassion: new FormControl(''),
    waitTimes: new FormControl(''),
    averageN: new FormControl('')
  });
  
  covidForm = new FormGroup({
    id: new FormControl(''),
    waitingRooms: new FormControl(''),
    protocols: new FormControl(''),
    separation: new FormControl(''),
    safety: new FormControl(''),
    covid1: new FormControl(''),
    screening: new FormControl(''),
    treatment: new FormControl(''),
    averageC: new FormControl('')
  });

  accommodationForm = new FormGroup({
    id: new FormControl(''),
    checkIn: new FormControl(''),
    discharge: new FormControl(''),
    equipment: new FormControl(''),
    policy: new FormControl(''),
    privacy: new FormControl(''),
    room: new FormControl(''),
    foodOptions: new FormControl(''),
    foodQuality: new FormControl(''),
    dietOptions: new FormControl(''),
    accessibility: new FormControl(''),
    parking: new FormControl(''),
    averageA: new FormControl('')
  });


  constructor(private reviewservice : ReviewService, public auth: AuthService, private hospitalservice : HospitalService, private _snackbar : MatSnackBar) { 


  }
  // , private formBuilder: FormBuilder


   ngOnInit():void  {
    this.setUser();
    this.GetHospitals();
    
  }
  GetHospitals()
  {
    this.hospitalservice.ListHospital().subscribe((hospitals) => {
      this.hospitals = hospitals;
    });
    }
  setUser(){
    this.auth.user$.subscribe(
      (res:any)=>{
        this.userId = res;
        this.reviewForm.patchValue({
          patientid: parseInt(res.sub.substring(6)),
        });
        console.log(parseInt(res.sub.substring(6)));

      }
    );

  }
   addReview(){
   
    this.reviewForm.patchValue({
      hospitalid: this.selected
    });

    this.reviewservice.AddReview(this.reviewForm .value).subscribe(
      res => {
        console.log(res);
        this.cleanlinessForm.patchValue({
          id: res
        });
        this.nursingForm.patchValue({
          id: res
        });
        this.covidForm.patchValue({
          id: res
        });
        this.accommodationForm.patchValue({
          id: res
        });
        this._snackbar.open('Review has been added', 'Successfully')
        this.reviewId = res;
        this.addCleanliness();
        this.addNursing();
        this.addCovid();
        this.addAccommodation();
      }
    );


  }

  addCleanliness(){
    this.reviewservice.AddCleanliness(this.cleanlinessForm.value).subscribe(
      res => {
      
      }
    );
  }

  addNursing(){
    this.reviewservice.AddNursing(this.nursingForm.value).subscribe(
      res => {
        
      }
    );
  }
  
addCovid(){
  this.reviewservice.AddCovid(this.covidForm.value).subscribe(
    res => {
      
    }
  );

}
addAccommodation(){
  this.reviewservice.AddAccommodation(this.accommodationForm.value).subscribe(
    res => {
     
    }
  );

}

  async addTotal(){

    this.addReview();


  }
}
