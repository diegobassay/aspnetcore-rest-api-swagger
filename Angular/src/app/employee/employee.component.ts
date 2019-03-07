import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './employee.service';
import { FormBuilder, FormGroup, FormControl, Validators } from "@angular/forms";
import { Employee } from './employee';
@Component({
  selector: 'app-employee', templateUrl: './employee.component.html', styleUrls: ['./employee.component.scss']
})
export class EmployeeComponent implements OnInit {

  constructor(public employeeService: EmployeeService, private formBuilder: FormBuilder) { }

  public employees: any[] = [];
  public createNew: boolean = true;
  public employeeForm: FormGroup;
  public showEmployeeModal: boolean = false;
  public showModalRemove: boolean = false;
  public employeeModalTitle: string = "";
  public textModalRemove: string = "";

  get form() { return this.employeeForm.controls; }

  ngOnInit() {
    this.list();
    this.employeeForm = this.formBuilder.group({
      id: [''],
      name: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      age: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  public openToSave() {
    this.showEmployeeModal = true;
    this.employeeModalTitle = `Criar Novo Funcionário`;
    this.employeeForm.reset();
  }

  public openToEdit(employee: Employee) {
    this.showEmployeeModal = true;
    this.employeeModalTitle = `Editar Funcionário`;
    this.employeeForm.patchValue(JSON.parse(JSON.stringify(employee)));
  }

  public openToDelete(employee: Employee) {
    this.showModalRemove = true;
    this.textModalRemove = `Deseja realmente remover o funcionário ${employee.name} ?`;
    this.employeeForm.patchValue(JSON.parse(JSON.stringify(employee)));
  }

  public submit() {
    if (this.employeeForm.value.id == null) {
         this.create();
    } else {
         this.update();
    }
  }

  public update() {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAsDirty();
    } else {
      this.showEmployeeModal = false;
      this.employeeService.update(this.employeeForm.value).subscribe(response => {
        this.list();
      }, error => {
        console.log(error);
      });
    }
  }

  public remove() {
    this.employeeService.remove(this.employeeForm.value).subscribe(response => {
      this.showModalRemove = false;
      this.list();
    }, error => {
      console.log(error);
    });
  }

  public create() {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAsDirty();
    } else {
      this.showEmployeeModal = false;
      delete this.employeeForm.value.id;
      this.employeeService.save(this.employeeForm.value).subscribe(response => {
        this.list();
      }, error => {
        console.log(error);
      });
    }
  }

  public list() {
    this.employeeService.getAll().subscribe(response => {
      this.employees = response;
      console.log(`Loaded employees...(${response.length})`);
    }, error => {
      console.log(error);
    });
  }
}
