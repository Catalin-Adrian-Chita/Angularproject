import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginPageComponent } from './login-page/login-page.component';
import { RegisterPageComponent } from './register-page/register-page.component';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
//modul pentru api
import { HttpClientModule } from '@angular/common/http';
//spinner
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { RegisterSuccessComponent } from './register-success/register-success.component';
import { validateVerticalPosition } from '@angular/cdk/overlay';
//navbar
import { MatToolbarModule } from '@angular/material/toolbar';
//menu
import { MatMenuModule } from '@angular/material/menu';
//icons
import { MatIconModule } from '@angular/material/icon';
import { FeedComponent } from './feed/feed.component';
const routes: Routes = [
  { path: 'login', component: LoginPageComponent },
  { path: 'feed', component: FeedComponent },
  { path: 'register', component: RegisterPageComponent },
  { path: 'viver', component: LandingPageComponent },
  { path: '', redirectTo: '/viver', pathMatch: 'full' },
];

@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    LoginPageComponent,
    RegisterPageComponent,
    RegisterSuccessComponent,
    FeedComponent,
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatButtonModule,
    RouterModule.forRoot(routes),
    ReactiveFormsModule,
    FormsModule,
    MatFormFieldModule,
    MatSelectModule,
    MatButtonToggleModule,
    MatInputModule,
    MatRadioModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    MatToolbarModule,
    MatIconModule,
    MatMenuModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatButtonToggleModule,
    MatSelectModule,
  ],
})
export class AppModule {}
