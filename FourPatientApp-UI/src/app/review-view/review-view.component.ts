import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Hospital } from '../interfaces/hospital';
import { Review } from '../interfaces/review';
import { ReviewService } from '../review.service';

@Component({
  selector: 'app-review-view',
  templateUrl: './review-view.component.html',
  styleUrls: ['./review-view.component.css']
})
export class ReviewViewComponent implements OnInit {


  review: Review[] | null = null;
  
  constructor(private route: ActivatedRoute, private reviewservices : ReviewService) { }

  ngOnInit(): void {
    this.GetReviewDetails();

  }
  GetReviewDetails() {
      const hospitalid = Number(this.route.snapshot.paramMap.get('id'));
      console.log(hospitalid);
  
      this.reviewservices.GetReviewbyHospitalId(hospitalid)
      .subscribe(review => this.review = review);
  
    }

}
