import ReactDOM from 'react-dom';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import AdminView from './Components/AdminView';
import OBSView from './Components/OBSView';

ReactDOM.render(
  <BrowserRouter>
    <Routes>
      <Route path="obs" element={<OBSView />} />
      <Route path="/" element={<AdminView />} />
    </Routes>
  </BrowserRouter>,
  document.getElementById('root')
);
