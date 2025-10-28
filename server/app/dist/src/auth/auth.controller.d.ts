import { AuthService } from './auth.service';
import { UsersService } from '../users/users.service';
export declare class AuthController {
    private readonly authService;
    private readonly usersService;
    constructor(authService: AuthService, usersService: UsersService);
    register(body: {
        email: string;
        password: string;
        name?: string;
    }): Promise<{
        accessToken: string;
        refreshToken: string;
    }>;
    login(req: any): Promise<{
        accessToken: string;
        refreshToken: string;
    }>;
    refresh(body: {
        refreshToken: string;
    }): Promise<{
        accessToken: string;
    }>;
    me(req: any): any;
}
