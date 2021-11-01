import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { UserService } from '../user.service';
import { Review } from '../interfaces/review';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profile:any;
  email = "";
  userId=0;
  FirstName="";
  LastName ="";
  City="";
  Street="";
  State="";
  Phone="";
  Zip="";
  reviews: Review[] = new Array(0);
  constructor(public auth: AuthService, public user: UserService) {
    
    

   }

  ngOnInit(): void {
    this.auth.user$.subscribe(
      (res:any)=>{
        this.profile = res;
        console.log(res);
        this.email = res.email;
        this.userId = parseInt(res.sub.substring(6));
        this.FirstName = res['http://localhost:4200/FirstName'];
        this.LastName = res['http://localhost:4200/LastName'];
        this.Phone = res['http://localhost:4200/Phone'];
        this.Zip = res['http://localhost:4200/Zip'];
        this.State = res['http://localhost:4200/State'];
        this.Street = res['http://localhost:4200/Street'];
        this.City = res['http://localhost:4200/City'];
        this.user.GetReviewbyPatientId(parseInt(res.sub.substring(6))).subscribe(
          (rev:any)=>{
            this.reviews = rev;
          }
        )
      }
    );
    

  }

}
