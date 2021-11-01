import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateReviewhComponent } from './create-reviewh.component';

describe('CreateReviewhComponent', () => {
  let component: CreateReviewhComponent;
  let fixture: ComponentFixture<CreateReviewhComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateReviewhComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateReviewhComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
