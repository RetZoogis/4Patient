import { Component, Input, OnInit } from '@angular/core';
import { HospitalService } from '../hospital.service';
import { Hospital } from '../interfaces/hospital';

@Component({
  selector: 'app-hospital-view',
  templateUrl: './hospital-view.component.html',
  styleUrls: ['./hospital-view.component.css'],

})
export class HospitalViewComponent implements OnInit {
  currentRate= 4.7;

  constructor(private hospitalservice: HospitalService) { }


  // @Input() starId;

  hospitals: Hospital[] = [];
  searchInput: string = "";
  ngOnInit(): void {
    this.GetHospitals();
  }
  search(){
   
    this.hospitalservice.SearchHospitals(this.searchInput).subscribe(
      (res:any)=>{
        console.log(res);
        this.hospitals = res;
        this.hospitals.sort((a,b) =>( a.comfort > b.comfort)? -1: 1)
      }

      
    );
  }
  GetHospitals()
  {
    this.hospitalservice.ListHospital().subscribe((hospitals) => {
      this.hospitals = hospitals;
      this.hospitals.sort((a,b) =>( a.comfort > b.comfort)? -1: 1)
    });
  }
  SortByAccomodations(){
    this.hospitals.sort((a,b) =>( a.accomodations > b.accomodations)? -1: 1)
  }
  SortByComfort(){
    this.hospitals.sort((a,b) =>( a.comfort > b.comfort)? -1: 1)
  }
  SortByNursing(){
    this.hospitals.sort((a,b) =>( a.nursing > b.nursing)? -1: 1)
  }
  SortByCleanliness(){
    this.hospitals.sort((a,b) =>( a.cleanliness > b.cleanliness)? -1: 1)
  }
  SortByName(){
    this.hospitals.sort((a,b) =>( a.name > b.name)? 1: -1)
  }
  // SortByNameReverse(){
  //   this.hospitals.sort((a,b) =>( a.name > b.name)? -1: 1)
  // }
    // To save Edit
    // save(): void {
    //   if (this.hero) {
    //     this.heroService.updateHero(this.hero)
    //       .subscribe(() => this.goBack());
    //   }

}
