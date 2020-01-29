import { Routes, RouterModule } from "@angular/router";
import { HomeComponent } from "./home/home.component";
import { FetchDataComponent } from "./fetch-data/fetch-data.component";
import { CounterComponent } from "./counter/counter.component";
import { NgModule } from "@angular/core";
import { LoginComponent } from "./core/login/login.component";
import { RegisterComponent } from "./core/register/register.component";
import { TodoComponent } from "./core/todo/todo.component";

const routes: Routes = [
    {path: '', component: HomeComponent, pathMatch: 'full' },
    { path: 'counter', component: CounterComponent },
    { path: 'todos', component: TodoComponent },
    { path: 'news', component: TodoComponent },
    { path: 'products', component: TodoComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: 'login' , component: LoginComponent },
    { path: 'register', component: RegisterComponent }
]

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule {}