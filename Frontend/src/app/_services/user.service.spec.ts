import { TestBed } from '@angular/core/testing';

import { UserService } from './user.service';

import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('UserServiceService', () => {
  let service: UserService;


  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(UserService);
  });

  it('should get list of users', () => {
    expect(service).toBeTruthy();
  });
});
