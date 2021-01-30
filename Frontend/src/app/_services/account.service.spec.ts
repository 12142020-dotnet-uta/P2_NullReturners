import { getTestBed, TestBed } from '@angular/core/testing';

import { AccountService } from './account.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserLoggingIn } from '../_models/UserLoggingIn';
import { UserLoggedIn } from '../_models/UserLoggedIn';

describe('AccountService', () => {
  let service: AccountService;
  let httpMock: HttpTestingController;
  let userLoggingIn: UserLoggingIn;
  let userLoggedIn: UserLoggedIn;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(AccountService);
    httpMock = getTestBed().inject(HttpTestingController);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call login()', () => {
    userLoggingIn = {
      username: "travis", password: "travis123"
    };
    //spyOn(service, "login")
    service.login(userLoggingIn);
    //expect(service.login).toHaveBeenCalled();

  });

  it('should call setCurrentUser()', () => {
    userLoggedIn = {
      userID: "hi", userName: "travis", fullName: "Travis Martin", 
      phoneNumber: "111-111-1111", email: "travis@gmail.com", 
      roleID: null, teamID: null
    };
    //spyOn(service, "login")
    service.setCurrentUser(userLoggedIn);
    //expect(service.login).toHaveBeenCalled();
  });

  it('should call logout()', () => {
    service.logout();
  });

});
