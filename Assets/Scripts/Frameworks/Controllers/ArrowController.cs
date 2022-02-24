using UnityEngine;
using Zenject;
using Usecases;
using Usecases.Commands;
using Frameworks.Dtos;


public class ArrowController : MonoBehaviour
{
    public IArrowDto _dto;

    [SerializeField] private LayerMask _layer;

    private DiContainer _container;


    [Inject]
    public void Construct(DiContainer container)
    {
        _container = container;
    }
    public void Awake()
    {
        _layer = LayerMask.GetMask(Entities.Arrow.ToString());
    }

    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _layer))
        {
            //Debug.DrawRay(ray.origin, ray.direction);
            if (hit.transform.CompareTag(Entities.Arrow.ToString()))
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        ArrowController controller = hit.transform.GetComponent<ArrowController>();
                        GameObject go = hit.transform.gameObject;
                        if (controller._dto._id == _dto._id)
                            _container.Resolve<DeleteArrow>().Execute(new DeleteArrowCommand(_dto._id));
                            Destroy(go);
                    }
                }
            }
        }
    }
}