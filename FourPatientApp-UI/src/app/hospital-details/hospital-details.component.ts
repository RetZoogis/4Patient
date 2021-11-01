import { Component, Input, OnInit, OnChanges } from '@angular/core';
import { HospitalService } from '../hospital.service';
import { Hospital } from '../interfaces/hospital';
import { ActivatedRoute } from '@angular/router';
import { ReviewService } from '../review.service';
import { Review } from '../interfaces/review';
import { CovidService } from '../covid.service';
import { Covid } from '../interfaces/covid';
@Component({
  selector: 'app-hospital-details',
  templateUrl: './hospital-details.component.html',
  styleUrls: ['./hospital-details.component.css']
})
export class HospitalDetailsComponent implements OnInit,  OnChanges {

  @Input() hospital?: Hospital 
  // @Input() review?: Review;
  
  reviews: Review[] | null = null;
  covid = {
    protocols: 0,
    waitingRooms: 0,
    separation: 0,
    safety: 0,
    screening: 0,
    treatment: 0
  } as Covid;
  constructor(private hospitalservice: HospitalService,  private route: ActivatedRoute, private reviewservices : ReviewService, private covidservices : CovidService) {
    

   }
   
  ngOnInit(): void {
    this.GetDetails();
    this.GetReviewDetails();
    
  }
  ngOnChanges(): void{
    this.GetCovidDetails();
  }
  GetDetails() {
    const hospitalid = Number(this.route.snapshot.paramMap.get('id'));
    console.log(hospitalid);
    this.hospitalservice.GetHospitalbyId(hospitalid)
    .subscribe(hospital => this.hospital = hospital);
    
  }

  GetReviewDetails() {
    const hospitalid = Number(this.route.snapshot.paramMap.get('id'));
    console.log(hospitalid);
    this.reviewservices.GetReviewbyHospitalId(hospitalid)
    .subscribe(reviews => {
      this.reviews = reviews;
      this.GetCovidDetails();
    });

  }
  GetCovidDetails() {
    if (this.reviews !== null) {
      let wr = 0;
      let wrTot = 0;
      let pr = 0;
      let prTot = 0;
      let sep = 0;
      let sepTot = 0;
      let saf = 0;
      let safTot = 0;
      let scr = 0;
      let scrTot = 0;
      let tr = 0;
      let trTot = 0;
      
      for (let i = 0; i < this.reviews.length; i++) {
        let reviewid = Number(this.reviews[i].id);
        this.covidservices.GetCovidById(reviewid).subscribe( 
          (res:Covid) =>{

          if(res.protocols != null){
            pr ++;
            prTot += res.protocols;
          }
          if(res.waitingRooms != null){
            wr ++;
            wrTot += res.waitingRooms;
          }
          if(res.separation != null){
            sep ++;
            sepTot += res.separation;
          }
          if(res.safety != null){
            saf ++;
            safTot += res.safety;
          }
          if(res.treatment != null){
            tr ++;
            trTot += res.treatment;
          }
          if(res.screening != null){
            scr ++;
            scrTot += res.screening;
          }
          if(pr != 0){
            this.covid.protocols = prTot/pr;
          }
          if(wr != 0){
            this.covid.waitingRooms = wrTot/wr;
          }
          if(sep != 0){
            this.covid.separation = sepTot/sep;
          }
          if(saf != 0){
            this.covid.safety = safTot/saf;
          }
          if(tr != 0){
            this.covid.treatment = trTot/tr;
          }
          if(scr != 0){
            this.covid.screening = scrTot/scr;
          }
        }
        
        );

      }
    }
  }


}
