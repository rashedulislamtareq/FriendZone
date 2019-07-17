import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-health-check',
  templateUrl: './health-check.component.html',
  styleUrls: ['./health-check.component.css']
})
export class HealthCheckComponent implements OnInit {
  application: any;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.doHealthCheck();
  }

  doHealthCheck() {
    this.http.get('http://localhost:5000/api/healthcheck').subscribe(response => {
      this.application = response;
    }, error => {
      console.log(error);
    }
    );
  }

}
