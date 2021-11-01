import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HttpClient, HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateReviewComponent } from './create-review/create-review.component';
import { LoginComponent } from './login/login.component';
import { AccountComponent } from './account/account.component';
import { HospitalViewComponent } from './hospital-view/hospital-view.component';


import { HospitalService } from './hospital.service';
import { HospitalDetailsComponent } from './hospital-details/hospital-details.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ProfileComponent } from './profile/profile.component';
import { MatSliderModule } from '@angular/material/slider';
import {MatStepperModule} from '@angular/material/stepper';
import {MatInputModule} from '@angular/material/input';
import {MatListModule} from '@angular/material/list';
import {MatChipsModule} from '@angular/material/chips';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import {MatButtonModule} from '@angular/material/button';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import {MatSelectModule} from '@angular/material/select';
import { ReviewViewComponent } from './review-view/review-view.component';
import { AuthModule } from '@auth0/auth0-angular';
import { environment as env } from '../environments/environment';
import { AuthGuard } from '@auth0/auth0-angular';
import { CreateReviewhComponent } from './create-reviewh/create-reviewh.component';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';



@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    CreateReviewComponent,
    LoginComponent,
    AccountComponent,
    HospitalViewComponent,
    ProfileComponent,
    HospitalDetailsComponent,
    ReviewViewComponent,
    CreateReviewhComponent,
  
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    BrowserModule,
    MatSelectModule,
    AuthModule.forRoot(
      {...env.auth},
    ),

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'create-review', component: CreateReviewComponent, canActivate: [AuthGuard]},
      { path: 'create-reviewh/:id', component: CreateReviewhComponent, canActivate: [AuthGuard]},
      
      { path: 'hospital-view', component: HospitalViewComponent },
      { path: 'account', component: AccountComponent },
      { path: 'details/:id', component: HospitalDetailsComponent },
      { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  
   
    ]),
    BrowserAnimationsModule,
    MatSliderModule,
    MatStepperModule,   
    MatIconModule,
    MatBottomSheetModule, 
    MatButtonModule,
    MatButtonToggleModule,
    MatSnackBarModule,
    MatChipsModule,
    MatInputModule,
    MatListModule,
    NgbModule,
 
  ],

  providers: [HospitalService],
  bootstrap: [AppComponent]
})
export class AppModule { }


