using OneriPlatform.BusinessLayer.Abstract;
using OneriPlatform.BusinessLayer.Results;
using OneriPlatform.Entities;
using OneriPlatform.Entities.Messages;
using OneriPlatform.Entities.ValueObjects;

namespace OneriPlatform.BusinessLayer
{
    public class SuggestionUserManager : ManagerBase<SuggestionUsers>
    {
        // Kullanıcı username kotrolü
        // Kullanıcı e-posta kontrolü..
        // Kayit İşlemi
        public BusinessLayerResultcs<SuggestionUsers> ResgisterUser(RegisterViewModel data)
        {
            SuggestionUsers user = Find(x => x.Username == data.Username || x.Email == data.EMail);
            BusinessLayerResultcs<SuggestionUsers> res = new BusinessLayerResultcs<SuggestionUsers>();
            if (user != null)
            {

                if (user.Username == data.Username)
                {
                    res.AddError(Entities.Messages.ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");

                }
                if (user.Email == data.EMail)
                {
                    res.AddError(Entities.Messages.ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

            }
            else
            {
                int dbResult = base.Insert(new SuggestionUsers()
                {
                    Username = data.Username,
                    Email = data.EMail,
                    Name = data.Username,
                    Password = data.Password
                });
                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Username == data.Username);
                    //Mail işlemleri yapıla bilir
                }
            }
            return res;
        }
        public BusinessLayerResultcs<SuggestionUsers> GetUserById(int id)
        {
            BusinessLayerResultcs<SuggestionUsers> res = new BusinessLayerResultcs<SuggestionUsers>();
            res.Result = Find(x => x.Id == id);
            if (res.Result == null)
            {
                res.AddError(Entities.Messages.ErrorMessageCode.UserCouldNotFind, "Kullanıcı Bulunamadı.");
            }
            return res;
        }
        public BusinessLayerResultcs<SuggestionUsers> LoginUser(LoginViewModel data)
        {
            BusinessLayerResultcs<SuggestionUsers> res = new BusinessLayerResultcs<SuggestionUsers>();
            res.Result = Find(x => x.Username == data.Username && x.Password == data.Password);
            if (res.Result == null)
            {
                res.AddError(Entities.Messages.ErrorMessageCode.UsernameAlreadyExists, "Kullancı Adı veya şifre hatalı");
            }
            return res;
        }
        public BusinessLayerResultcs<SuggestionUsers> UpdateProfile(SuggestionUsers data)
        {
            SuggestionUsers db_user = Find(x => x.Id == data.Id);
            BusinessLayerResultcs<SuggestionUsers> res = new BusinessLayerResultcs<SuggestionUsers>();
            if (db_user != null)
            {
                res.AddError(Entities.Messages.ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı. değildi");
                return res;
            }
            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            if (base.Update(res.Result) == 0)
            {
                res.AddError(Entities.Messages.ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");

            }
            return res;
        }
        public BusinessLayerResultcs<SuggestionUsers> RemoveUserById(int id)
        {
            BusinessLayerResultcs<SuggestionUsers> res = new BusinessLayerResultcs<SuggestionUsers>();
            SuggestionUsers user = Find(x => x.Id == id);
            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(Entities.Messages.ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return res;
                }
            }
            else
            {
                res.AddError(Entities.Messages.ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }
            return res;
        }
        public new BusinessLayerResultcs<SuggestionUsers> Insert(SuggestionUsers data)
        {
            SuggestionUsers user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResultcs<SuggestionUsers> res = new BusinessLayerResultcs<SuggestionUsers>();
            res.Result = data;
            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(Entities.Messages.ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                if (base.Insert(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı eklenemedi.");
                }
            }
            return res;
        }
        public new BusinessLayerResultcs<SuggestionUsers> Update(SuggestionUsers data)
        {
            SuggestionUsers db_user = Find(x => x.Username == data.Username || x.Email == data.Email);
            BusinessLayerResultcs<SuggestionUsers> res = new BusinessLayerResultcs<SuggestionUsers>();
            res.Result = data;
            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;

            }
            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı Güncelleme İşlemi Gerçekleştirilemedi");
            }
            return res;
        }
    }
}