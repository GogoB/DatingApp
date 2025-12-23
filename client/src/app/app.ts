import { HttpClient } from '@angular/common/http';
import { Component, signal, inject, OnInit, Signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit{
  private http = inject(HttpClient);
  protected readonly title = 'Dating App';
  protected members = signal<any>([]);

//2 of the ways to approach getting data 
//using subscribe
async  ngOnInit(){
//    //small chance that the subscription can get stuck
//    this.http.get('https://localhost:5001/api/members').subscribe({
//      next: response => this.members.set(response),
//      error: error => console.log(error),
//      complete: () => console.log('Completed the http request')
//    });
    this.members.set(await this.getMembers())
  }

  async getMembers(){
    try{
      //promise is depricated so we use lastValueFrom and firstValueFrom
      return lastValueFrom (this.http.get('https://localhost:5001/api/members'));
    } catch(error){
      console.log(error);
      throw error
    }
  }
  
}
