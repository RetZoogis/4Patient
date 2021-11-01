
import { RouterModule } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { DOCUMENT } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit{

  isExpanded = false;

  constructor(public auth: AuthService, @Inject(DOCUMENT) private doc: Document) {
  }

  collapse() {
    this.isExpanded = false;
  }
  ngOnInit(): void {}
  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  loginWithRedirect(): void {
    this.auth.loginWithRedirect({screen_hint: 'signup' });
  }
  logout(): void {
    this.auth.logout({ returnTo: this.doc.location.origin });
  }
}