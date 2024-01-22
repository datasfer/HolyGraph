import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'HolyGraph.Client';
  books: any;
  host = "http://localhost:9101/api/scriptures"
  constructor(private http: HttpClient){}

  ngOnInit(): void {
    this.http.get(this.host).subscribe({
      next: response =>  {
        this.books = response; 
        this.books.forEach((element: any) => {
          //console.log(element);
        });
      },
      error: error => console.log(error),
      complete: () => console.log("Request has completed.")
    });
}
}
