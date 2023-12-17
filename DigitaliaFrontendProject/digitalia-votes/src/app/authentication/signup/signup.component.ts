import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { UserRegister } from 'src/app/core/models/user';
import { AuthService } from 'src/app/core/service/auth.service';
@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  authForm: UntypedFormGroup;
  submitted = false;
  returnUrl: string;
  hide = true;
  chide = true;
  userRq :UserRegister={
    userID: 0,
    userName: '',
    email: '',
    passwordHash: '',
    authProvider:'',
    providerID:'',
    profilePictureUrl:'',
    creationDate: new Date(),
    lastAccess:null
  }
  constructor(
    private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {}
  ngOnInit() {
    this.authForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: [
        '',
        [Validators.required, Validators.email, Validators.minLength(5)],
      ],
      password: ['', Validators.required],
     
    });
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }
  get f() {
    return this.authForm.controls;
  }
  onSubmit() {
    console.log("this.authForm", this.authForm.value);
    this.userRq.userName = this.authForm.value.username;
    this.userRq.email = this.authForm.value.email;
    this.userRq.passwordHash = this.authForm.value.password;
    this.authService.registerUser(this.userRq).subscribe(res =>{
      if(res.objModel > 0){
        this.router.navigate(['/authentication/signin']);
      }
    })    
  }
}
