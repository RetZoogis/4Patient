
import packageInfo from '../../auth_config.json';

export const environment = {
  production: true,
  BaseURL: 'https://20.72.170.145/fourpatient-frontend',
  apiUrl: 'https://20.72.170.145/fourpatient-webapi/api',
  auth: {
    domain: packageInfo.domain,
    clientId: packageInfo.clientId,
    redirectUri: document.baseURI
  },
};
