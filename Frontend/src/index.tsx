import { SnackbarProvider } from 'notistack';
import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AdminView from './Views/AdminView';

ReactDOM.render(
  <SnackbarProvider
    anchorOrigin={{
      horizontal: 'left',
      vertical: 'top'
    }}
    autoHideDuration={3000}
    dense
    hideIconVariant
  >
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<AdminView />} />
      </Routes>
    </BrowserRouter>
  </SnackbarProvider>,
  document.getElementById('root')
);
