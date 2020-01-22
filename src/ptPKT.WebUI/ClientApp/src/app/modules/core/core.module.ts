import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from 'src/app/core/login/login.component';
import { HomeComponent } from 'src/app/home/home.component';
import { RegisterComponent } from 'src/app/core/register/register.component';
import { FetchDataComponent } from 'src/app/fetch-data/fetch-data.component';
import { CounterComponent } from 'src/app/counter/counter.component';
import { MaterialModule } from '../material/material.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from 'src/app/app.routing.module';
import { TodoComponent } from '../../core/todo/todo.component';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    ReactiveFormsModule,
    AppRoutingModule,
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    FetchDataComponent,
    CounterComponent,
    TodoComponent
  ]
})
export class CoreModule { }
