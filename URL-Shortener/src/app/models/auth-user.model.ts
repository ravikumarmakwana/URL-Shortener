export interface AuthUser {
    accessToken: string;
    refreshToken: string;
    refreshTokenExpiryTime: string;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    phoneNumber: string;
}