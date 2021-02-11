import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Viver';

  token;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.token = localStorage.getItem('viverToken');

    if (this.token) this.router.navigate(['/feed']);
    else this.router.navigate(['/viver']);
  }
}
