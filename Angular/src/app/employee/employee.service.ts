import { Injectable, Component, Inject  } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Employee } from './employee';
import { Observable } from 'rxjs/Observable';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient, @Inject('API_URL') private apiURL: string) {
    
  }

  getAll() {
    return this.http.get<Employee[]>(this.apiURL + 'api/employee');
  }
  
  save(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.apiURL + 'api/employee', employee);
  }
  
  update(employee: Employee): Observable<Employee> {
    return this.http.put<Employee>(this.apiURL + `api/employee/${employee.id}`, employee);
  }
  
  remove(employee: Employee): Observable<Employee> {
    return this.http.delete<Employee>(this.apiURL + `api/employee/${employee.id}`);
  }

}
