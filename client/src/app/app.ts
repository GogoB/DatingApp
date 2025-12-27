import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { Nav } from "../layout/nav/nav";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Nav],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App{
  protected router = inject(Router);
  
//2 of the ways to approach getting data 
//using subscribe
async  ngOnInit(){
//    //small chance that the subscription can get stuck
//    this.http.get('https://localhost:5001/api/members').subscribe({
//      next: response => this.members.set(response),
//      error: error => console.log(error),
//      complete: () => console.log('Completed the http request')
//    });

//    console.log("MEMBERS CHECK!!!!!",this.members)
  }
}
